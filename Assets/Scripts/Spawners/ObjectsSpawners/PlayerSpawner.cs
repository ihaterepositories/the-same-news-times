using Controllers.InGameControllers;
using Models;
using UnityEngine;
using UnityEngine.Serialization;

namespace Spawners.ObjectsSpawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private Player playerPrefab;

        private ObjectPool<Player> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<Player>(playerPrefab);
        }

        public void Spawn(Vector2 position)
        {
            var player = GetPlayerObject();
            player.transform.localPosition = position;
        }

        private Player GetPlayerObject()
        {
            var poolAble = _pool.GetFreeObject();
            return poolAble as Player;
        }
    }
}
