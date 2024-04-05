using Controllers.InGameControllers;
using MazeGeneration;
using Models.Items;
using Pooling;
using UnityEngine;
using Zenject;

namespace Spawners.ItemsSpawners
{
    public class KeySpawner : MonoBehaviour
    {
        [SerializeField] private Key keyPrefab;

        private ObjectPool<Key> _pool;
        private PositionsBlocker _positionsBlocker;
        
        [Inject]
        private void Construct(PositionsBlocker positionsBlocker)
        {
            _positionsBlocker = positionsBlocker;
        }

        private void Awake()
        {
            _pool = new ObjectPool<Key>(keyPrefab);
        }

        public void Spawn(Cell[,] maze, int mazeWidth, int mazeHeight)
        {
            while (true)
            {
                var xPosition = Random.Range(1, mazeWidth - 1);
                var yPosition = Random.Range(1, mazeHeight - 1);

                if (xPosition != MazeGenerator.ExitCell.X && 
                    yPosition != MazeGenerator.ExitCell.Y && 
                    _positionsBlocker.CheckPositionAvailability(xPosition, yPosition))
                {
                    var cell = maze[xPosition, yPosition];
                    var key = GetKeyObject();
                    key.transform.localPosition = MazeSpawner.GetCellWorldCoordinates(cell, mazeWidth, mazeHeight);

                    _positionsBlocker.BlockPosition(xPosition, yPosition, true);
                }
                else
                {
                    continue;
                }

                break;
            }
        }

        private Key GetKeyObject()
        {
            var poolAble = _pool.GetFreeObject();
            return poolAble as Key;
        }
    }
}
