using UnityEngine;
using UnityEngine.UIElements;

public class GreenScoresSpawner : MonoBehaviour
{
    [SerializeField] private GreenScore _greenScorePrefab;

    private ObjectPool<GreenScore> _pool;
    private int _greenScoresCount;

    public int GreenScoresCount 
    { 
        get { return _greenScoresCount; } 
        set { _greenScoresCount = value; } 
    }

    private void Awake()
    {
        _pool = new ObjectPool<GreenScore>(_greenScorePrefab);
    }

    public void Spawn(Cell[,] maze, int mazeWidth, int mazeHeight, int iterationsCoeficient = 1)
    {
        int iterations = Random.Range(mazeWidth - 10, mazeHeight - 5) * iterationsCoeficient;
        _greenScoresCount = 0;

        for (int i = 0; i < iterations; i++)
        {
            int xPosition = Random.Range(1, mazeWidth - 1);
            int yPosition = Random.Range(1, mazeHeight - 1);

            if (xPosition != MazeGenerator.ExitCell.x && 
                yPosition != MazeGenerator.ExitCell.y &&
                PositionBlockController.CheckPositionAvailability(xPosition, yPosition))
            {
                Cell cell = maze[xPosition, yPosition];
                GreenScore greenScore = GetGreenScoreObject();
                greenScore.transform.localPosition = MazeSpawner.GetWorldCellCoordinates(cell, mazeWidth, mazeHeight);
                _greenScoresCount++;

                PositionBlockController.BlockPosition(xPosition, yPosition, false);
            }
            else { continue; }
        }
    }

    public void LuckySpawn(Cell[,] maze, int mazeWidth, int mazeHeight)
    {
        _greenScoresCount = 0;

        for (int i = 0; i < mazeWidth - 1; i++)
        {
            for (int j = 0; j < mazeHeight - 1; j++)
            {
                Cell cell = maze[i, j];

                if ((cell.x == MazeGenerator.ExitCell.x && cell.y == MazeGenerator.ExitCell.y) ||
                    (cell.x == 0 && cell.y == 0))
                {
                    continue;
                }
                else
                {
                    GreenScore greenScore = GetGreenScoreObject();
                    greenScore.transform.localPosition = MazeSpawner.GetWorldCellCoordinates(cell, mazeWidth, mazeHeight);
                    _greenScoresCount++;
                }
            }
        }
    }

    private GreenScore GetGreenScoreObject()
    {
        IPoolable poolable = _pool.GetFreeObject();
        return poolable as GreenScore;
    }
}
