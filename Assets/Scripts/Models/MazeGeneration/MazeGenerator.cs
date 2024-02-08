using System.Collections.Generic;

public class MazeGenerator
{
    private int width;
    private int height;

    public static Cell ExitCell {  get; private set; }

    public MazeGenerator (int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public Cell[,] Generate()
    {
        Cell[,] maze = new Cell[width, height];

        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                maze[i, j] = new Cell { x = i, y = j };
            }
        }

        RemoveExtraWalls(maze);
        GenerateWay(maze);
        PlaceMazeExit(maze);

        return maze;
    }

    private void RemoveExtraWalls(Cell[,] maze)
    {
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            maze[x, height - 1].isHaveLeftWall = false;
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            maze[width - 1, y].isHaveBottomtWall = false;
        }
    }

    private void GenerateWay(Cell[,] labyrinths)
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

    private void PlaceMazeExit(Cell[,] maze)
    {
        Cell furthest = maze[0, 0];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            if (maze[x, height - 2].distanceFromStartPoint > furthest.distanceFromStartPoint) furthest = maze[x, height - 2];
            if (maze[x, 0].distanceFromStartPoint > furthest.distanceFromStartPoint) furthest = maze[x, 0];
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            if (maze[width - 2, y].distanceFromStartPoint > furthest.distanceFromStartPoint) furthest = maze[width - 2, y];
            if (maze[0, y].distanceFromStartPoint > furthest.distanceFromStartPoint) furthest = maze[0, y];
        }

        if (furthest.x == 0) furthest.isHaveLeftWall = false;
        else if (furthest.y == 0) furthest.isHaveBottomtWall = false;
        else if (furthest.x == width - 2) maze[furthest.x + 1, furthest.y].isHaveLeftWall = false;
        else if (furthest.y == height - 2) maze[furthest.x, furthest.y + 1].isHaveBottomtWall = false;

        ExitCell = new Cell();
        ExitCell.x = furthest.x;
        ExitCell.y = furthest.y;
    }
}
