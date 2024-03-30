using Controllers.InGameControllers;
using Models.Items;
using Models.MazeGeneration;
using UnityEngine;

namespace Spawners.ObjectsSpawners
{
    public class LifeSaverSpawner : MonoBehaviour
    {
        [SerializeField] private LifeSaver lifeSaverPrefab;

        private ObjectPool<LifeSaver> _pool;

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
                    PositionBlocker.CheckPositionAvailability(xPosition, yPosition))
                {
                    var cell = maze[xPosition, yPosition];
                    var lifeSaver = GetKeyObject();
                    lifeSaver.transform.localPosition = MazeSpawner.GetCellWorldCoordinates(cell, mazeWidth, mazeHeight);

                    PositionBlocker.BlockPosition(xPosition, yPosition, true);
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