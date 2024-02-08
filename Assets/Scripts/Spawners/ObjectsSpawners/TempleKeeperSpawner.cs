using UnityEngine;

public class TempleKeeperSpawner : MonoBehaviour
{
    [SerializeField] private TempleKeeper _templeKeeperPrefab;
    private ObjectPool<TempleKeeper> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<TempleKeeper>(_templeKeeperPrefab);
    }

    public void Spawn(Cell[,] maze, int mazeWidth, int mazeHeight)
    {
        int spawnPositionX = Random.Range(5, mazeWidth - 1);
        int spawnPositionY = Random.Range(5, mazeHeight - 1);

        if (spawnPositionX != MazeGenerator.ExitCell.x &&
            spawnPositionY != MazeGenerator.ExitCell.y)
        {
            Cell cell = maze[spawnPositionX, spawnPositionY];
            TempleKeeper templeKeeper = GetTempleKeeperObject();
            templeKeeper.transform.localPosition = MazeSpawner.GetWorldCellCoordinates(cell, mazeWidth, mazeHeight);
            templeKeeper.MakeEnemySleep();
        }
        else { Spawn(maze, mazeWidth, mazeHeight); }
    }

    private TempleKeeper GetTempleKeeperObject()
    {
        IPoolable poolable = _pool.GetFreeObject();
        return poolable as TempleKeeper;
    }
}
