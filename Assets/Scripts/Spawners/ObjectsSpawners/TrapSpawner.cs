using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    [SerializeField] private Trap _trapPrefab;

    private ObjectPool<Trap> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Trap>(_trapPrefab);
    }

    public void Spawn(Cell[,] maze, int mazeWidth, int mazeHeight)
    {
        int iterations = Random.Range(mazeWidth - 15, mazeHeight - 5);

        for (int i = 0; i < iterations; i++)
        {
            int xPosition = Random.Range(1, mazeWidth - 1);
            int yPosition = Random.Range(1, mazeHeight - 1);

            if (xPosition != MazeGenerator.ExitCell.x &&
                yPosition != MazeGenerator.ExitCell.y &&
                PositionBlockController.CheckPositionAvailability(xPosition, yPosition))
            {
                Cell cell = maze[xPosition, yPosition];
                Trap trap = GetTrapObject();
                trap.transform.localPosition = MazeSpawner.GetWorldCellCoordinates(cell, mazeWidth, mazeHeight);
                trap.PlayAppearingAnimation();

                PositionBlockController.BlockPosition(xPosition, yPosition, true);
            }
            else { continue; }
        }
    }

    private Trap GetTrapObject()
    {
        IPoolable poolable = _pool.GetFreeObject();
        return poolable as Trap;
    }
}
