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
        int xPosition = Random.Range(5, mazeWidth - 1);
        int yPosition = Random.Range(5, mazeHeight - 1);

        if (xPosition != MazeGenerator.ExitCell.x &&
            yPosition != MazeGenerator.ExitCell.y &&
            PositionBlockController.CheckPositionAvailability(xPosition, yPosition))
        {
            Cell cell = maze[xPosition, yPosition];
            TempleKeeper templeKeeper = GetTempleKeeperObject();
            templeKeeper.transform.localPosition = MazeSpawner.GetWorldCellCoordinates(cell, mazeWidth, mazeHeight);
            templeKeeper.MakeEnemySleep();

            PositionBlockController.BlockPosition(xPosition, yPosition, true);
        }
        else { Spawn(maze, mazeWidth, mazeHeight); }
    }

    private TempleKeeper GetTempleKeeperObject()
    {
        IPoolable poolable = _pool.GetFreeObject();
        return poolable as TempleKeeper;
    }
}
