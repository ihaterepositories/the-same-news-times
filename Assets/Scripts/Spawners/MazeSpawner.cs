using System.Collections.Generic;
using Controllers.InGameControllers;
using Models;
using Models.MazeGeneration;
using UnityEngine;
using UnityEngine.Serialization;

namespace Spawners
{
    public class MazeSpawner : MonoBehaviour
    {
        [SerializeField] private CellWallsCollector cellPrefab;

        private ObjectPool<CellWallsCollector> _pool;

        public Vector2 FirstCellCoordinates => new(-(MazeWidth / 2f) + 0.9f, -(MazeHeight / 2f) + 0.9f);
        public int MazeWidth { get; private set; }
        public int MazeHeight { get; private set; }
        public int SpawnedCyclesCount { get; private set; }
        public Cell[,] Maze { get; private set; }
        public List<CellWallsCollector> CellObjects { get; private set; }

        private void Awake()
        {
            _pool = new ObjectPool<CellWallsCollector>(cellPrefab);
        }
        
        public void Spawn(int cyclesCount)
        {
            MazeWidth = Random.Range(20, 36);
            MazeHeight = Random.Range(15, 19);
            Maze = new MazeGenerator(MazeWidth, MazeHeight).Generate();
            CellObjects = new List<CellWallsCollector>();
            CreateCycles(Maze, cyclesCount);
            SpawnCells(Maze);
        }

        public void Spawn(int cyclesCount, int mazeWidth, int mazeHeight)
        {
            MazeWidth = mazeWidth;
            MazeHeight = mazeHeight;
            Maze = new MazeGenerator(mazeWidth, mazeHeight).Generate();
            CellObjects = new List<CellWallsCollector>();
            CreateCycles(Maze, cyclesCount);
            SpawnCells(Maze);
        }

        // Delete some random walls, which are closer to the maze center, to create cycles
        private void CreateCycles(Cell[,] maze, int cyclesCount)
        {
            for (int i = 0; i < cyclesCount; i++)
            {
                int cellPositionX = Random.Range(3, MazeWidth - 2);
                int cellPositionY = Random.Range(3, MazeHeight - 2);

                if (maze[cellPositionX, cellPositionY].IsHaveLeftWall)
                {
                    maze[cellPositionX, cellPositionY].IsHaveLeftWall = false;
                    SpawnedCyclesCount++;
                    continue;
                }

                if (maze[cellPositionX, cellPositionY].IsHaveBottomWall)
                {
                    maze[cellPositionX, cellPositionY].IsHaveBottomWall = false;
                    SpawnedCyclesCount++;
                }
            }
        }
        
        private void SpawnCells(Cell[,] maze)
        {
            for (int cellPositionX = 0; cellPositionX < maze.GetLength(0); cellPositionX++)
            {
                for (int cellPositionY = 0; cellPositionY < maze.GetLength(1); cellPositionY++)
                {
                    CellWallsCollector cell = GetCellObject();
                    cell.transform.localPosition = new Vector2(
                        cellPositionX - (MazeWidth / 2f) + 0.5f, 
                        cellPositionY - (MazeHeight / 2f) + 0.5f);

                    cell.ChangeTransparency(0);

                    cell.leftWall.SetActive(maze[cellPositionX, cellPositionY].IsHaveLeftWall);
                    cell.bottomWall.SetActive(maze[cellPositionX, cellPositionY].IsHaveBottomWall);

                    CellObjects.Add(cell);
                }
            }
        }

        private CellWallsCollector GetCellObject()
        {
            var poolAble = _pool.GetFreeObject();
            return poolAble as CellWallsCollector;
        }
        
        public static Vector2 GetCellWorldCoordinates(Cell cell, int mazeWidth, int mazeHeight)
        {
            return new Vector2(cell.X - (mazeWidth / 2f) + 0.9f, cell.Y - (mazeHeight / 2f) + 0.9f);
        }
    }
}
