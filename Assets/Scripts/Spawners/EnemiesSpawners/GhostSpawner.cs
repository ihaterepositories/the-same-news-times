using Loaders;
using MazeGeneration;
using Models.Enemies;
using Pooling;
using UnityEngine;
using Zenject;

namespace Spawners.EnemiesSpawners
{
    public class GhostSpawner : MonoBehaviour
    {
        private ObjectPool<Ghost> _pool;
        private PrefabsLoader _prefabsLoader;

        [Inject]
         private void Construct(PrefabsLoader prefabsLoader)
         {
             _prefabsLoader = prefabsLoader;
         }
        
        private void Awake()
        {
            _pool = new ObjectPool<Ghost>(_prefabsLoader.GetPrefab("Ghost").GetComponent<Ghost>());
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
