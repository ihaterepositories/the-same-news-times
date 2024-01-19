using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private CellWallsCollector _cellPrefab;
    [SerializeField] private GameObject _exitObjectPrefab;
    [SerializeField] private GameObject _greenPointPrefab;
    [SerializeField] private GameObject _enemyPrefab;

    private int _spawnedCyclesCount;
    private int _mazeWidth;
    private int _mazeHeight;
    private Cell[,] _maze;
    private List<CellWallsCollector> _cellObjects;
    private ObjectPool<CellWallsCollector> _pool;

    public Vector2 FirstCellCoordinates { get { return new Vector2(-(_mazeWidth / 2) + 0.9f, -(_mazeHeight / 2) + 0.9f); } }
    public int MazeWidth { get { return _mazeWidth; } }
    public int MazeHeight { get { return _mazeHeight; } }
    public int SpawnedCyclesCount { get { return _spawnedCyclesCount; } }
    public Cell[,] Maze { get { return _maze; } }
    public List<CellWallsCollector> CellObjects { get { return _cellObjects; } }

    private void Awake()
    {
        _pool = new ObjectPool<CellWallsCollector>(_cellPrefab);
    }

    public void Spawn()
    {
        _mazeWidth = Random.Range(20, 36);
        _mazeHeight = Random.Range(15, 19);
        _maze = new MazeGenerator(_mazeWidth, _mazeHeight).Generate();
        _cellObjects = new List<CellWallsCollector>();
        CreateCycles(_maze, UnityEngine.Random.Range(1, 5));
        SpawnCells(_maze);
    }

    private void SpawnCells(Cell[,] maze)
    {
        for (int cellPositionX = 0; cellPositionX < maze.GetLength(0); cellPositionX++)
        {
            for (int cellPositionY = 0; cellPositionY < maze.GetLength(1); cellPositionY++)
            {
                CellWallsCollector cell = GetCellObject();
                cell.transform.localPosition = new Vector2(
                    cellPositionX - (_mazeWidth / 2) + 0.5f, 
                    cellPositionY - (_mazeHeight / 2) + 0.5f);

                cell.ChangeTransparety(0);

                cell.leftWall.SetActive(maze[cellPositionX, cellPositionY].isHaveLeftWall);
                cell.bottomWall.SetActive(maze[cellPositionX, cellPositionY].isHaveBottomtWall);

                _cellObjects.Add(cell);
            }
        }
    }

    private CellWallsCollector GetCellObject()
    {
        IPoolable poolable = _pool.GetFreeObject();
        return poolable as CellWallsCollector;
    }

    // Delete some random walls, which are closer to the maze center, to create cycles
    private void CreateCycles(Cell[,] maze, int cyclesCount)
    {
        for (int i = 0; i < cyclesCount; i++)
        {
            int cellPositionX = UnityEngine.Random.Range(3, _mazeWidth - 2);
            int cellPositionY = UnityEngine.Random.Range(3, _mazeHeight - 2);

            if (maze[cellPositionX, cellPositionY].isHaveLeftWall)
            {
                maze[cellPositionX, cellPositionY].isHaveLeftWall = false;
                _spawnedCyclesCount++;
                continue;
            }

            if (maze[cellPositionX, cellPositionY].isHaveBottomtWall)
            {
                maze[cellPositionX, cellPositionY].isHaveBottomtWall = false;
                _spawnedCyclesCount++;
            }
        }
    }

    public static Vector2 GetWorldCellCoordinates(Cell cell, int mazeWidth, int mazeHeight)
    {
        return new Vector2(cell.x - (mazeWidth / 2) + 0.9f, cell.y - (mazeHeight / 2) + 0.9f);
    }
}
