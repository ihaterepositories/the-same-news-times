using Loaders;
using MazeGeneration;
using Models.Items;
using Pooling;
using UnityEngine;
using Zenject;

namespace Spawners.ItemsSpawners
{
    public class LockSpawner : MonoBehaviour
    {
        private ObjectPool<Lock> _pool;
        private PrefabsLoader _prefabsLoader;
        
        [Inject]
        private void Construct(PrefabsLoader prefabsLoader)
        {
            _prefabsLoader = prefabsLoader;
        }

        private void Awake()
        {
            _pool = new ObjectPool<Lock>(_prefabsLoader.GetPrefab("Lock").GetComponent<Lock>());
        }

        public void Spawn(int mazeWidth, int mazeHeight)
        {
            var lockObject = GetLockObject();
            lockObject.transform.localPosition = MazeSpawner.GetCellWorldCoordinates(MazeGenerator.ExitCell, mazeWidth, mazeHeight);
        }

        private Lock GetLockObject()
        {
            var poolAble = _pool.GetFreeObject();
            return poolAble as Lock;
        }
    }
}
