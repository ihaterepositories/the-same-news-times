using Controllers.InGameControllers;
using MazeGeneration;
using Models.Items;
using Pooling;
using UnityEngine;
using Zenject;

namespace Spawners.ItemsSpawners
{
    public class BoosterSpawner : MonoBehaviour
    {
        [SerializeField] private Booster boosterPrefab;

        private ObjectPool<Booster> _pool;
        private PositionsBlocker _positionsBlocker;
        
        [Inject]
        private void Construct(PositionsBlocker positionsBlocker)
        {
            _positionsBlocker = positionsBlocker;
        }

        private void Awake()
        {
            _pool = new ObjectPool<Booster>(boosterPrefab);
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
                    var booster = GetKeyObject();
                    booster.transform.localPosition = MazeSpawner.GetCellWorldCoordinates(cell, mazeWidth, mazeHeight);

                    _positionsBlocker.BlockPosition(xPosition, yPosition, true);
                }
                else
                {
                    continue;
                }

                break;
            }
        }

        private Booster GetKeyObject()
        {
            var poolAble = _pool.GetFreeObject();
            return poolAble as Booster;
        }    
    }
}