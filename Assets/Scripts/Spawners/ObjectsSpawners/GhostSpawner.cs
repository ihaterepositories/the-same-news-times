using Controllers.InGameControllers;
using Models;
using Models.Enemies;
using Models.MazeGeneration;
using UnityEngine;

namespace Spawners.ObjectsSpawners
{
    public class GhostSpawner : MonoBehaviour
    {
        [SerializeField] private Ghost ghostPrefab;
        
        private ObjectPool<Ghost> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<Ghost>(ghostPrefab);
        }

        public void Spawn(Cell[,] maze, int mazeWidth, int mazeHeight)
        {
            var spawnPositionX = Random.Range(2, mazeWidth - 1);
            var spawnPositionY = Random.Range(2, mazeHeight - 1);

            var cell = maze[spawnPositionX, spawnPositionY];
            var ghost = GetGhostObject();
            ghost.transform.localPosition = MazeSpawner.GetCellWorldCoordinates(cell, mazeWidth, mazeHeight);
            ghost.StartHunting();
        }

        private Ghost GetGhostObject()
        {
            var poolAble = _pool.GetFreeObject();
            return poolAble as Ghost;
        }
    }
}
