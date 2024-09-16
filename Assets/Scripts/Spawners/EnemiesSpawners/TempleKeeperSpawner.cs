using Controllers.InGameControllers;
using Loaders;
using MazeGeneration;
using Models.Enemies;
using Pooling;
using UnityEngine;
using Zenject;

namespace Spawners.EnemiesSpawners
{
    public class TempleKeeperSpawner : MonoBehaviour
    {
        private ObjectPool<TempleKeeper> _pool;
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
            _pool = new ObjectPool<TempleKeeper>(_prefabsLoader.GetPrefab("TempleKeeper").GetComponent<TempleKeeper>());
        }

        public void Spawn(Cell[,] maze, int mazeWidth, int mazeHeight)
        {
            while (true)
            {
                var xPosition = Random.Range(5, mazeWidth - 1);
                var yPosition = Random.Range(5, mazeHeight - 1);

                if (xPosition != MazeGenerator.ExitCell.X && 
                    yPosition != MazeGenerator.ExitCell.Y && 
                    _positionsBlocker.CheckPositionAvailability(xPosition, yPosition))
                {
                    var cell = maze[xPosition, yPosition];
                    var templeKeeper = GetTempleKeeperObject();
                    templeKeeper.transform.localPosition = MazeSpawner.GetCellWorldCoordinates(cell, mazeWidth, mazeHeight);
                    templeKeeper.MakeEnemySleep();

                    _positionsBlocker.Block(xPosition, yPosition, true);
                }
                else
                {
                    continue;
                }

                break;
            }
        }

        private TempleKeeper GetTempleKeeperObject()
        {
            var poolAble = _pool.GetFreeObject();
            return poolAble as TempleKeeper;
        }
    }
}
