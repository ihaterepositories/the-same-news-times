using Loaders;
using MazeGeneration;
using Models.Items;
using Pooling;
using UnityEngine;
using Zenject;

namespace Spawners.ItemsSpawners
{
    public class MazeExitSpawner : MonoBehaviour
    {
        private ObjectPool<MazeExit> _pool;
        private PrefabsLoader _prefabsLoader;
        
        [Inject]
        private void Construct(PrefabsLoader prefabsLoader)
        {
            _prefabsLoader = prefabsLoader;
        }

        private void Awake()
        {
            _pool = new ObjectPool<MazeExit>(_prefabsLoader.GetPrefab("MazeExit").GetComponent<MazeExit>());
        }

        public void Spawn(int mazeWidth, int mazeHeight)
        {
            var pinkScore = GetPinkScoreObject();
            pinkScore.transform.localPosition = MazeSpawner.GetCellWorldCoordinates(MazeGenerator.ExitCell, mazeWidth, mazeHeight);
        }

        private MazeExit GetPinkScoreObject()
        {
            var poolAble = _pool.GetFreeObject();
            return poolAble as MazeExit;
        }
    }
}
