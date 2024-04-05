using Controllers.InGameControllers;
using MazeGeneration;
using Models.Items;
using Pooling;
using UnityEngine;
using Zenject;

namespace Spawners.ItemsSpawners
{
    public class GreenScoresSpawner : MonoBehaviour
    {
        [SerializeField] private GreenScore greenScorePrefab;

        private ObjectPool<GreenScore> _pool;
        private int _greenScoresCount;
        private PositionsBlocker _positionsBlocker;
        
        [Inject]
        private void Construct(PositionsBlocker positionsBlocker)
        {
            _positionsBlocker = positionsBlocker;
        }

        public int GreenScoresCount { get => _greenScoresCount; set => _greenScoresCount = value; }

        private void Awake()
        {
            _pool = new ObjectPool<GreenScore>(greenScorePrefab);
        }

        public void Spawn(Cell[,] maze, int mazeWidth, int mazeHeight, int iterationsCoefficient = 1)
        {
            var iterations = Random.Range(mazeWidth - 10, mazeHeight - 5) * iterationsCoefficient;
            _greenScoresCount = 0;

            for (var i = 0; i < iterations; i++)
            {
                var xPosition = Random.Range(1, mazeWidth - 1);
                var yPosition = Random.Range(1, mazeHeight - 1);

                if (xPosition != MazeGenerator.ExitCell.X && 
                    yPosition != MazeGenerator.ExitCell.Y &&
                    _positionsBlocker.CheckPositionAvailability(xPosition, yPosition))
                {
                    var cell = maze[xPosition, yPosition];
                    var greenScore = GetGreenScoreObject();
                    greenScore.transform.localPosition = MazeSpawner.GetCellWorldCoordinates(cell, mazeWidth, mazeHeight);
                    _greenScoresCount++;

                    _positionsBlocker.BlockPosition(xPosition, yPosition, false);
                }
            }
        }

        public void LuckySpawn(Cell[,] maze, int mazeWidth, int mazeHeight)
        {
            _greenScoresCount = 0;

            for (var i = 0; i < mazeWidth - 1; i++)
            {
                for (var j = 0; j < mazeHeight - 1; j++)
                {
                    var cell = maze[i, j];

                    if ((cell.X == MazeGenerator.ExitCell.X && cell.Y == MazeGenerator.ExitCell.Y) ||
                        (cell.X == 0 && cell.Y == 0))
                    {
                        continue;
                    }

                    var greenScore = GetGreenScoreObject();
                    greenScore.transform.localPosition = MazeSpawner.GetCellWorldCoordinates(cell, mazeWidth, mazeHeight);
                    _greenScoresCount++;
                }
            }
        }

        private GreenScore GetGreenScoreObject()
        {
            var poolAble = _pool.GetFreeObject();
            return poolAble as GreenScore;
        }
    }
}
