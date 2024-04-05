using Controllers.InGameControllers;
using MazeGeneration;
using Models.Items;
using Pooling;
using UnityEngine;
using Zenject;

namespace Spawners.ItemsSpawners
{
    public class LifeSaverSpawner : MonoBehaviour
    {
        [SerializeField] private LifeSaver lifeSaverPrefab;

        private ObjectPool<LifeSaver> _pool;
        private PositionsBlocker _positionsBlocker;
        
        [Inject]
        private void Construct(PositionsBlocker positionsBlocker)
        {
            _positionsBlocker = positionsBlocker;
        }

        private void Awake()
        {
            _pool = new ObjectPool<LifeSaver>(lifeSaverPrefab);
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
                    var lifeSaver = GetKeyObject();
                    lifeSaver.transform.localPosition = MazeSpawner.GetCellWorldCoordinates(cell, mazeWidth, mazeHeight);

                    _positionsBlocker.BlockPosition(xPosition, yPosition, true);
                }
                else
                {
                    continue;
                }

                break;
            }
        }

        private LifeSaver GetKeyObject()
        {
            var poolAble = _pool.GetFreeObject();
            return poolAble as LifeSaver;
        }
    }
}