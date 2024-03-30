using Controllers.InGameControllers;
using Models.Items;
using Models.MazeGeneration;
using UnityEngine;

namespace Spawners.ObjectsSpawners
{
    public class LockSpawner : MonoBehaviour
    {
        [SerializeField] private Lock lockPrefab;

        private ObjectPool<Lock> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<Lock>(lockPrefab);
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
