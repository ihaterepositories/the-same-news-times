using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    [SerializeField] private Key _keyPrefab;

    private ObjectPool<Key> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Key>(_keyPrefab);
    }

    public void Spawn(Cell[,] maze, int mazeWidth, int mazeHeight)
    {
        int xPosition = Random.Range(1, mazeWidth - 1);
        int yPosition = Random.Range(1, mazeHeight - 1);

        if (xPosition != MazeGenerator.ExitCell.x &&
            yPosition != MazeGenerator.ExitCell.y &&
            PositionBlockController.CheckPositionAvailability(xPosition, yPosition))
        {
            Cell cell = maze[xPosition, yPosition];
            Key key = GetKeyObject();
            key.transform.localPosition = MazeSpawner.GetWorldCellCoordinates(cell, mazeWidth, mazeHeight);

            PositionBlockController.BlockPosition(xPosition, yPosition, true);
        }
        else { Spawn(maze, mazeWidth, mazeHeight); }
    }

    private Key GetKeyObject()
    {
        IPoolable poolable = _pool.GetFreeObject();
        return poolable as Key;
    }
}
