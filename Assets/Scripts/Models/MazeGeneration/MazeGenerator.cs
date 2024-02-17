using System.Collections.Generic;

namespace Models.MazeGeneration
{
    public class MazeGenerator
    {
        private readonly int _width;
        private readonly int _height;

        public static Cell ExitCell {  get; private set; }

        public MazeGenerator (int width, int height)
        {
            _width = width;
            _height = height;
        }

        public Cell[,] Generate()
        {
            var maze = new Cell[_width, _height];

            for (var i = 0; i < maze.GetLength(0); i++)
            {
                for (var j = 0; j < maze.GetLength(1); j++)
                {
                    maze[i, j] = new Cell { X = i, Y = j };
                }
            }

            RemoveExtraWalls(maze);
            GenerateWay(maze);
            PlaceMazeExit(maze);

            return maze;
        }

        private void RemoveExtraWalls(Cell[,] maze)
        {
            for (var x = 0; x < maze.GetLength(0); x++)
            {
                maze[x, _height - 1].IsHaveLeftWall = false;
            }

            for (var y = 0; y < maze.GetLength(1); y++)
            {
                maze[_width - 1, y].IsHaveBottomWall = false;
            }
        }

        private void GenerateWay(Cell[,] labyrinths)
        {
            var currentCell = labyrinths[0, 0];
            currentCell.IsVisitedByGenerator = true;
            currentCell.DistanceFromStartPoint = 0;

            var stack = new Stack<Cell>();

            do
            {
                var unvisitedNeighbourCells = new List<Cell>();

                var x = currentCell.X;
                var y = currentCell.Y;

                if (x > 0 && !labyrinths[x - 1, y].IsVisitedByGenerator) unvisitedNeighbourCells.Add(labyrinths[x - 1, y]);
                if (y > 0 && !labyrinths[x, y - 1].IsVisitedByGenerator) unvisitedNeighbourCells.Add(labyrinths[x, y - 1]);
                if (x < _width - 2 && !labyrinths[x + 1, y].IsVisitedByGenerator) unvisitedNeighbourCells.Add(labyrinths[x + 1, y]);
                if (y < _height - 2 && !labyrinths[x, y + 1].IsVisitedByGenerator) unvisitedNeighbourCells.Add(labyrinths[x, y + 1]);

                if (unvisitedNeighbourCells.Count > 0)
                {
                    var chosen = unvisitedNeighbourCells[UnityEngine.Random.Range(0, unvisitedNeighbourCells.Count)];
                    RemoveWall(currentCell, chosen);

                    chosen.IsVisitedByGenerator = true;
                    stack.Push(chosen);
                    currentCell = chosen;
                    chosen.DistanceFromStartPoint = stack.Count;
                }
                else currentCell = stack.Pop();
            }
            while (stack.Count > 0);
        }

        private void RemoveWall(Cell a, Cell b)
        {
            if (a.X == b.X)
            {
                if (a.Y > b.Y) a.IsHaveBottomWall = false;
                else b.IsHaveBottomWall = false;
            }
            else
            {
                if (a.X > b.X) a.IsHaveLeftWall = false;
                else b.IsHaveLeftWall = false;
            }
        }

        private void PlaceMazeExit(Cell[,] maze)
        {
            var furthest = maze[0, 0];

            for (var x = 0; x < maze.GetLength(0); x++)
            {
                if (maze[x, _height - 2].DistanceFromStartPoint > furthest.DistanceFromStartPoint) furthest = maze[x, _height - 2];
                if (maze[x, 0].DistanceFromStartPoint > furthest.DistanceFromStartPoint) furthest = maze[x, 0];
            }

            for (var y = 0; y < maze.GetLength(1); y++)
            {
                if (maze[_width - 2, y].DistanceFromStartPoint > furthest.DistanceFromStartPoint) furthest = maze[_width - 2, y];
                if (maze[0, y].DistanceFromStartPoint > furthest.DistanceFromStartPoint) furthest = maze[0, y];
            }

            if (furthest.X == 0) furthest.IsHaveLeftWall = false;
            else if (furthest.Y == 0) furthest.IsHaveBottomWall = false;
            else if (furthest.X == _width - 2) maze[furthest.X + 1, furthest.Y].IsHaveLeftWall = false;
            else if (furthest.Y == _height - 2) maze[furthest.X, furthest.Y + 1].IsHaveBottomWall = false;

            ExitCell = new Cell
            {
                X = furthest.X,
                Y = furthest.Y
            };
        }
    }
}
