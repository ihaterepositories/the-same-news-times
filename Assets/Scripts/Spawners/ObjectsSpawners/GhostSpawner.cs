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
        int spawnPositionX = Random.Range(2, mazeWidth - 1);
        int spawnPositionY = Random.Range(2, mazeHeight - 1);

        Cell cell = maze[spawnPositionX, spawnPositionY];
        Ghost ghost = GetGhostObject();
        ghost.transform.localPosition = MazeSpawner.GetWorldCellCoordinates(cell, mazeWidth, mazeHeight);
        ghost.StartHunting();
    }

    private Ghost GetGhostObject()
    {
        IPoolable poolable = _pool.GetFreeObject();
        return poolable as Ghost;
    }
}
