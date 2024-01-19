using UnityEngine;

public class GreenScoresSpawner : MonoBehaviour
{
    [SerializeField] private GreenScore _greenScorePrefab;

    private ObjectPool<GreenScore> _pool;
    private int _greenScoresCount;

    public int GreenScoresCount { get { return _greenScoresCount; } }

    private void Awake()
    {
        _pool = new ObjectPool<GreenScore>(_greenScorePrefab);
    }

    public void Spawn(Cell[,] maze, int mazeWidth, int mazeHeight)
    {
        _greenScoresCount = Random.Range(mazeWidth - 10, mazeHeight - 5);

        for (int i = 0; i < _greenScoresCount; i++)
        {
            int xPosition = Random.Range(1, mazeWidth - 1);
            int yPosition = Random.Range(1, mazeHeight - 1);

            if (xPosition != MazeGenerator.ExitCell.x && 
                yPosition != MazeGenerator.ExitCell.y)
            {
                Cell cell = maze[xPosition, yPosition];

                GreenScore greenScore = GetGreenScoreObject();
                greenScore.transform.localPosition = MazeSpawner.GetWorldCellCoordinates(cell, mazeWidth, mazeHeight);
            }
            else { _greenScoresCount--; }
        }
    }

    private GreenScore GetGreenScoreObject()
    {
        IPoolable poolable = _pool.GetFreeObject();
        return poolable as GreenScore;
    }
}
