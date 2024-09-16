using Controllers.InGameControllers;
using DataTransfers;
using Loaders;
using MazeGeneration;
using Models.Items;
using Pooling;
using UnityEngine;
using Zenject;

namespace Spawners.ItemsSpawners
{
    public class ModificatorsSpawner : MonoBehaviour
    {
        private ObjectPool<Modificator> _pool;
        private PositionsBlocker _positionsBlocker;
        private PrefabsLoader _prefabsLoader;

        [Inject]
        private void Construct(PositionsBlocker positionsBlocker, PrefabsLoader prefabsLoader)
        {
            _positionsBlocker = positionsBlocker;
            _prefabsLoader = prefabsLoader;
        }
        
        private void Awake()
        {
            _pool = new ObjectPool<Modificator>(_prefabsLoader.GetPrefab("Modificator").GetComponent<Modificator>());
        }

        public void Spawn(MazeData mazeData, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var coordinates = GenerateRandomSpawnCoordinates(mazeData);
                
                var cellToSpawnObjectIn = mazeData.Cells[coordinates.x, coordinates.y];
                var modificator = GetModificatorFromPool();
                var realCoordinates = MazeSpawner.GetCellWorldCoordinates(cellToSpawnObjectIn, mazeData.Width, mazeData.Height);

                modificator.transform.localPosition = realCoordinates;

                _positionsBlocker.Block(coordinates.x, coordinates.y, true);
            }
        }

        private Vector2Int GenerateRandomSpawnCoordinates(MazeData mazeData)
        {
            var xPosition = Random.Range(1, mazeData.Width - 1);
            var yPosition = Random.Range(1, mazeData.Height - 1);

            if (((xPosition == MazeGenerator.ExitCell.X) && (yPosition == MazeGenerator.ExitCell.Y))
                || _positionsBlocker.CheckPositionAvailability(xPosition, yPosition) == false)
            {
                GenerateRandomSpawnCoordinates(mazeData);
            }
            
            return new Vector2Int(xPosition, yPosition);
        }
        
        private Modificator GetModificatorFromPool()
        {
            var poolAble = _pool.GetFreeObject();
            return poolAble as Modificator;
        }
    }
}