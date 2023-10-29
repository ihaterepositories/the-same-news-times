using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;

    private int spawnedCyclesCount;
    private int mazeSize;

    public Vector2 FirstCellCoordinates { get { return new Vector2(-(mazeSize / 2) + 0.9f, -(mazeSize / 2) + 0.9f); } }
    public int MazeSize { get { return mazeSize; } }
    public int SpawnedCyclesCount { get { return spawnedCyclesCount; } }

    public void Spawn()
    {
        mazeSize = UnityEngine.Random.Range(10, 19);
        var mazeGenerator = new MazeGenerator(mazeSize, mazeSize);
        var maze = mazeGenerator.Generate();
        var cells = new List<CellWallsCollector>();

        CreateCycles(maze, UnityEngine.Random.Range(1, 5));

        InstantiateCells(maze, cells);
    }

    private void InstantiateCells(Cell[,] maze, List<CellWallsCollector> cells)
    {
        for (int cellPositionX = 0; cellPositionX < maze.GetLength(0); cellPositionX++)
        {
            for (int cellPositionY = 0; cellPositionY < maze.GetLength(1); cellPositionY++)
            {
                CellWallsCollector cell = Instantiate(
                    cellPrefab, 
                    new Vector2(cellPositionX - (mazeSize / 2) + 0.5f, cellPositionY - (mazeSize / 2) + 0.5f), 
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
            int cellPositionX = UnityEngine.Random.Range(3, mazeSize - 2);
            int cellPositionY = UnityEngine.Random.Range(3, mazeSize - 2);

            if (maze[cellPositionX, cellPositionY].isHaveLeftWall)
            {
                maze[cellPositionX, cellPositionY].isHaveLeftWall = false;
                spawnedCyclesCount++;
                continue;
            }

            if (maze[cellPositionX, cellPositionY].isHaveBottomtWall)
            {
                maze[cellPositionX, cellPositionY].isHaveBottomtWall = false;
                spawnedCyclesCount++;
            }
        }
    }

    private IEnumerator CellSpawnAnimationCoroutine(CellWallsCollector cell)
    {
        yield return new WaitForSeconds(0.0005f);
        ChangeCellTransparety(cell, 0.1f);
        yield return new WaitForSeconds(0.0005f);
        ChangeCellTransparety(cell, 0.2f);
        yield return new WaitForSeconds(0.0005f);
        ChangeCellTransparety(cell, 0.3f);
        yield return new WaitForSeconds(0.0005f);
        ChangeCellTransparety(cell, 0.4f);
        yield return new WaitForSeconds(0.0005f);
        ChangeCellTransparety(cell, 0.5f);
        yield return new WaitForSeconds(0.0005f);
        ChangeCellTransparety(cell, 0.6f);
        yield return new WaitForSeconds(0.0005f);
        ChangeCellTransparety(cell, 0.7f);
        yield return new WaitForSeconds(0.0005f);
        ChangeCellTransparety(cell, 0.8f);
        yield return new WaitForSeconds(0.0005f);
        ChangeCellTransparety(cell, 0.9f);
        yield return new WaitForSeconds(0.0005f);
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
}
