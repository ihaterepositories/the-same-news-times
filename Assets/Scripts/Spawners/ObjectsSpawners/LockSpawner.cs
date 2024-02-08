using UnityEngine;

public class LockSpawner : MonoBehaviour
{
    [SerializeField] private Lock _lockPrefab;

    private ObjectPool<Lock> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Lock>(_lockPrefab);
    }

    public void Spawn(int mazeWitdh, int mazeHeight)
    {
        Lock lockObject = GetLockObject();
        lockObject.transform.localPosition = MazeSpawner.GetWorldCellCoordinates(MazeGenerator.ExitCell, mazeWitdh, mazeHeight);
    }

    private Lock GetLockObject()
    {
        IPoolable poolable = _pool.GetFreeObject();
        return poolable as Lock;
    }
}
