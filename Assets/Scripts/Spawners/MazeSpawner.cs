using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private GameObject _exitObjectPrefab;
    [SerializeField] private GameObject _greenPointPrefab;
    [SerializeField] private GameObject _enemyPrefab;

    private int _spawnedCyclesCount;
    private int _spawnedEatablePointsCount;
    private int _mazeWidth = 35;
    private int _mazeHeight = 18;
    private Cell[,] _maze;

    public Vector2 FirstCellCoordinates { get { return new Vector2(-(_mazeWidth / 2) + 0.9f, -(_mazeHeight / 2) + 0.9f); } }
    public int MazeWidth { get { return _mazeWidth; } }
    public int MazeHeight { get { return _mazeHeight; } }
    public int SpawnedCyclesCount { get { return _spawnedCyclesCount; } }
    public int SpawnedEatablePointsCount { get { return _spawnedEatablePointsCount; } }

    public void Spawn()
    {
        _mazeWidth = UnityEngine.Random.Range(20, 36);
        _mazeHeight = UnityEngine.Random.Range(15, 19);
        var mazeGenerator = new MazeGenerator(_mazeWidth, _mazeHeight);
        _maze = mazeGenerator.Generate();
        var cells = new List<CellWallsCollector>();

        CreateCycles(_maze, UnityEngine.Random.Range(1, 5));
        SpawnCells(_maze, cells);
        SpawnExitObject();
        SpawnEatablePoints();
        SpawnEnemies();
    }

    private void SpawnCells(Cell[,] maze, List<CellWallsCollector> cells)
    {
        for (int cellPositionX = 0; cellPositionX < maze.GetLength(0); cellPositionX++)
        {
            for (int cellPositionY = 0; cellPositionY < maze.GetLength(1); cellPositionY++)
            {
                CellWallsCollector cell = Instantiate(
                    _cellPrefab, 
                    new Vector2(cellPositionX - (_mazeWidth / 2) + 0.5f, cellPositionY - (_mazeHeight / 2) + 0.5f), 
                    Quaternion.identity)
                    .GetComponent<CellWallsCollector>();

                ChangeCellTransparety(cell, 0f);

                cell.leftWall.SetActive(maze[cellPositionX, cellPositionY].isHaveLeftWall);
                cell.bottomWall.SetActive(maze[cellPositionX, cellPositionY].isHaveBottomtWall);
                cell.gameObject.name = $"cell[X:{cellPositionX}][Y:{cellPositionY}]";

                cells.Add(cell);
            }
        }

        StartCoroutine(PlayCellsSpawnAnimation(cells));
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

    private IEnumerator CellSpawnAnimationCoroutine(CellWallsCollector cell)
    {
        yield return new WaitForSeconds(0.0001f);
        ChangeCellTransparety(cell, 0.4f);
        yield return new WaitForSeconds(0.0001f);
        ChangeCellTransparety(cell, 0.5f);
        yield return new WaitForSeconds(0.0001f);
        ChangeCellTransparety(cell, 0.6f);
        yield return new WaitForSeconds(0.0001f);
        ChangeCellTransparety(cell, 0.7f);
        yield return new WaitForSeconds(0.0001f);
        ChangeCellTransparety(cell, 0.8f);
        yield return new WaitForSeconds(0.0001f);
        ChangeCellTransparety(cell, 0.9f);
        yield return new WaitForSeconds(0.0001f);
        ChangeCellTransparety(cell, 1);
    }

    private void ChangeCellTransparety(CellWallsCollector cell, float transparety)
    {
        cell.bottomWallSpriteRenderer.color = new Color(
                    cell.bottomWallSpriteRenderer.color.r,
                    cell.bottomWallSpriteRenderer.color.g,
                    cell.bottomWallSpriteRenderer.color.b,
                    transparety);

        cell.leftWallSpriteRenderer.color = new Color(
                    cell.bottomWallSpriteRenderer.color.r,
                    cell.bottomWallSpriteRenderer.color.g,
                    cell.bottomWallSpriteRenderer.color.b,
                    transparety);
    }

    private void ShuffleCells(List<CellWallsCollector> cells)
    {
        System.Random rng = new System.Random();
        int n = cells.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            CellWallsCollector value = cells[k];
            cells[k] = cells[n];
            cells[n] = value;
        }
    }

    private IEnumerator PlayCellsSpawnAnimation(List<CellWallsCollector> cells)
    {
        ShuffleCells(cells);

        for (int i = 0; i < cells.Count; i++)
        {
            StartCoroutine(CellSpawnAnimationCoroutine(cells[i]));
            yield return new WaitForSeconds(0.0001f);
        }
    }

    private void SpawnExitObject()
    {
        Instantiate(
            _exitObjectPrefab,
            new Vector2(MazeGenerator.ExitCellPositionX - (_mazeWidth / 2) + 0.9f, MazeGenerator.ExitCellPositionY - (_mazeHeight / 2) + 0.9f),
            Quaternion.identity);
    }

    private void SpawnEatablePoints()
    {
        int eatablePointsCount = UnityEngine.Random.Range(_mazeWidth - 10, _mazeHeight - 5);
        _spawnedEatablePointsCount = eatablePointsCount;

        for (int i = 0; i < eatablePointsCount; i++)
        {
            int xPosition = UnityEngine.Random.Range(0, _mazeWidth - 1);
            int yPosition = UnityEngine.Random.Range(0, _mazeHeight - 1);

            if (xPosition != MazeGenerator.ExitCellPositionX && yPosition != MazeGenerator.ExitCellPositionY)
            {
                Cell cell = _maze[xPosition, yPosition];

                Instantiate(
                _greenPointPrefab,
                new Vector2(cell.x - (_mazeWidth / 2) + 0.9f, cell.y - (_mazeHeight / 2) + 0.9f),
                Quaternion.identity);
            }
            else
            {
                _spawnedEatablePointsCount--;
            }
        }
    }

    private void SpawnEnemies()
    {
        int enemiesCount = UnityEngine.Random.Range(1, 4);

        for (int i = 0; i < enemiesCount; i++)
        {
            int xPosition = UnityEngine.Random.Range(0, _mazeWidth - 1);
            int yPosition = UnityEngine.Random.Range(0, _mazeHeight - 1);

            if (xPosition != MazeGenerator.ExitCellPositionX && yPosition != MazeGenerator.ExitCellPositionY)
            {
                Cell cell = _maze[xPosition, yPosition];

                Instantiate(
                _enemyPrefab,
                new Vector2(cell.x - (_mazeWidth / 2) + 0.9f, cell.y - (_mazeHeight / 2) + 0.9f),
                Quaternion.identity);
            }
        }
    }
}
