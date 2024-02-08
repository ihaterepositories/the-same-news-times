using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Player _playerPrefab;

    private ObjectPool<Player> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Player>(_playerPrefab);
    }

    public void Spawn(Vector2 position)
    {
        Player player = GetPlayerObject();
        player.transform.localPosition = position;
    }

    private Player GetPlayerObject()
    {
        IPoolable poolable = _pool.GetFreeObject();
        return poolable as Player;
    }
}
