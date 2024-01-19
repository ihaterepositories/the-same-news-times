using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component, IPoolable
{
    public readonly List<IPoolable> _freeObjects;
    private readonly Transform _container;
    private readonly T _prefab;

    public ObjectPool(T prefab)
    {
        _freeObjects = new List<IPoolable>();
        _container = new GameObject().transform;
        _container.name = prefab.GameObject.name;
        _prefab = prefab;
    }

    public IPoolable GetFreeObject()
    {
        IPoolable poolable;

        if (_freeObjects.Count > 0)
        {
            poolable = _freeObjects[0] as T;
            _freeObjects.RemoveAt(0);
        }
        else
        {
            poolable = Object.Instantiate(_prefab, _container);
        }

        poolable.GameObject.SetActive(true);
        poolable.OnDestroyed += ReturnToPool;
        
        return poolable;
    }

    private void ReturnToPool(IPoolable poolable)
    {
        _freeObjects.Add(poolable);
        poolable.OnDestroyed -= ReturnToPool;
        poolable.GameObject.SetActive(false);
        poolable.GameObject.transform.SetParent(_container);
    }
}
