using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator
{
    private int width;
    private int height;

    public static int ExitCellPositionX { get; private set; }
    public static int ExitCellPositionY { get; private set; }

    public MazeGenerator (int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public Cell[,] Generate()
    {
        var labyrinths = new Cell[width, height];

        for (int i = 0; i < labyrinths.GetLength(0); i++)
        {
            for (int j = 0; j < labyrinths.GetLength(1); j++)
            {
                labyrinths[i, j] = new Cell { x = i, y = j };
            }
        }

        // remove walls, which out of the maze
        for (int x = 0; x < labyrinths.GetLength(0); x++)
        {
            labyrinths[x, height - 1].isHaveLeftWall = false;
        }

        for (int y = 0; y < labyrinths.GetLength(1); y++)
        {
            labyrinths[width - 1, y].isHaveBottomtWall = false;
        }
        //

        RemoveWalls(labyrinths);

        PlaceMazeExit(labyrinths);

        return labyrinths;
    }

    public void RemoveWalls(Cell[,] labyrinths)
    {
        Cell currentCell = labyrinths[0, 0];
        currentCell.isVisitedByGenerator = true;
        currentCell.distanceFromStartPoint = 0;

        Stack<Cell> stack = new Stack<Cell>();

        do
        {
            List<Cell> unvisitedNeighbourCells = new List<Cell>();

            int x = currentCell.x;
            int y = currentCell.y;

            if (x > 0 && !labyrinths[x - 1, y].isVisitedByGenerator) unvisitedNeighbourCells.Add(labyrinths[x - 1, y]);
            if (y > 0 && !labyrinths[x, y - 1].isVisitedByGenerator) unvisitedNeighbourCells.Add(labyrinths[x, y - 1]);
            if (x < width - 2 && !labyrinths[x + 1, y].isVisitedByGenerator) unvisitedNeighbourCells.Add(labyrinths[x + 1, y]);
            if (y < height - 2 && !labyrinths[x, y + 1].isVisitedByGenerator) unvisitedNeighbourCells.Add(labyrinths[x, y + 1]);

            if (unvisitedNeighbourCells.Count > 0)
            {
                Cell chosen = unvisitedNeighbourCells[UnityEngine.Random.Range(0, unvisitedNeighbourCells.Count)];
                RemoveWall(currentCell, chosen);

                chosen.isVisitedByGenerator = true;
                stack.Push(chosen);
                currentCell = chosen;
                chosen.distanceFromStartPoint = stack.Count;
            }
            else
            {
                currentCell = stack.Pop();
            }
        }
        while (stack.Count > 0);
    }

    private void RemoveWall(Cell a, Cell b)
    {
        if (a.x == b.x)
        {
            if (a.y > b.y) a.isHaveBottomtWall = false;
            else b.isHaveBottomtWall = false;
        }
        else
        {
            if (a.x > b.x) a.isHaveLeftWall = false;
            else b.isHaveLeftWall = false;
        }
    }

    private void PlaceMazeExit(Cell[,] labyrinths)
    {
        Cell furthest = labyrinths[0, 0];

        for (int x = 0; x < labyrinths.GetLength(1); x++)
        {
            if (labyrinths[x, height - 2].distanceFromStartPoint > furthest.distanceFromStartPoint) furthest = labyrinths[x, height - 2];
            if (labyrinths[x, 0].distanceFromStartPoint > furthest.distanceFromStartPoint) furthest = labyrinths[x, 0];
        }

        for (int y = 0; y < labyrinths.GetLength(1); y++)
        {
            if (labyrinths[width - 2, y].distanceFromStartPoint > furthest.distanceFromStartPoint) furthest = labyrinths[width - 2, y];
            if (labyrinths[0, y].distanceFromStartPoint > furthest.distanceFromStartPoint) furthest = labyrinths[0, y];
        }

        if (furthest.x == 0) furthest.isHaveLeftWall = false;
        else if (furthest.y == 0) furthest.isHaveBottomtWall = false;
        else if (furthest.x == width - 2) labyrinths[furthest.x + 1, furthest.y].isHaveLeftWall = false;
        else if (furthest.y == height - 2) labyrinths[furthest.x, furthest.y + 1].isHaveBottomtWall = false;

        ExitCellPositionX = furthest.x;
        ExitCellPositionY = furthest.y;
    }
}
