using UnityEngine;

public class PinkScoreSpawner : MonoBehaviour
{
    [SerializeField] private PinkScore _pinkScorePrefab;
    private ObjectPool<PinkScore> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<PinkScore>(_pinkScorePrefab);
    }

    public void Spawn(int mazeWitdh, int mazeHeight)
    {
        PinkScore pinkScore = GetPinkScoreObject();
        pinkScore.transform.localPosition = MazeSpawner.GetWorldCellCoordinates(MazeGenerator.ExitCell, mazeWitdh, mazeHeight);
    }

    private PinkScore GetPinkScoreObject()
    {
        IPoolable poolable = _pool.GetFreeObject();
        return poolable as PinkScore;
    }
}
