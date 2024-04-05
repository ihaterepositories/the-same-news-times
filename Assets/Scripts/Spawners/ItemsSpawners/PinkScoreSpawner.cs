using MazeGeneration;
using Models.Items;
using Pooling;
using UnityEngine;

namespace Spawners.ItemsSpawners
{
    public class PinkScoreSpawner : MonoBehaviour
    {
        [SerializeField] private PinkScore pinkScorePrefab;
        
        private ObjectPool<PinkScore> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<PinkScore>(pinkScorePrefab);
        }

        public void Spawn(int mazeWidth, int mazeHeight)
        {
            var pinkScore = GetPinkScoreObject();
            pinkScore.transform.localPosition = MazeSpawner.GetCellWorldCoordinates(MazeGenerator.ExitCell, mazeWidth, mazeHeight);
        }

        private PinkScore GetPinkScoreObject()
        {
            var poolAble = _pool.GetFreeObject();
            return poolAble as PinkScore;
        }
    }
}
