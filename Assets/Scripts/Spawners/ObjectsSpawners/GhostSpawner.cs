using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] private Ghost _ghostPrefab;
    private ObjectPool<Ghost> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Ghost>(_ghostPrefab);
    }

    public void Spawn(Cell[,] maze, int mazeWidth, int mazeHeight)
    {
        int spawnPositionX = Random.Range(5, mazeWidth - 1);
        int spawnPositionY = Random.Range(5, mazeHeight - 1);

        if (spawnPositionX != MazeGenerator.ExitCell.x &&
            spawnPositionY != MazeGenerator.ExitCell.y)
        {
            Cell cell = maze[spawnPositionX, spawnPositionY];
            Ghost ghost = GetGhostObject();
            ghost.transform.localPosition = MazeSpawner.GetWorldCellCoordinates(cell, mazeWidth, mazeHeight);

            ghost.SetMaxPositions(
                MazeSpawner.GetWorldCellCoordinates(maze[mazeWidth-1, mazeHeight-1], mazeWidth, mazeHeight).x,
                MazeSpawner.GetWorldCellCoordinates(maze[mazeWidth-1, mazeHeight-1], mazeWidth, mazeHeight).y);

            ghost.SetMinPositions(
                MazeSpawner.GetWorldCellCoordinates(maze[0, 0], mazeWidth, mazeHeight).x,
                MazeSpawner.GetWorldCellCoordinates(maze[0, 0], mazeWidth, mazeHeight).y);

            ghost.StartHunting();
        }
        else { Spawn(maze, mazeWidth, mazeHeight); }
    }

    private Ghost GetGhostObject()
    {
        IPoolable poolable = _pool.GetFreeObject();
        return poolable as Ghost;
    }
}
