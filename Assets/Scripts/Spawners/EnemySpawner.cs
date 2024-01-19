using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(_enemyPrefab);
    }

    public void Spawn(Cell[,] maze, int mazeWidth, int mazeHeight)
    {
        int spawnPositionX = Random.Range(5, mazeWidth - 1);
        int spawnPositionY = Random.Range(5, mazeHeight - 1);

        if (spawnPositionX != MazeGenerator.ExitCell.x &&
            spawnPositionY != MazeGenerator.ExitCell.y)
        {
            Cell cell = maze[spawnPositionX, spawnPositionY];
            Enemy enemy = GetEenemyObject();
            enemy.transform.localPosition = MazeSpawner.GetWorldCellCoordinates(cell, mazeWidth, mazeHeight);
        }
        else { Spawn(maze, mazeWidth, mazeHeight); }
    }

    private Enemy GetEenemyObject()
    {
        IPoolable poolable = _pool.GetFreeObject();
        return poolable as Enemy;
    }
}
