using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Models
{
    public class ObjectPool<T> where T : Component, IPoolAble
    {
        private readonly List<IPoolAble> _freeObjects;
        private readonly Transform _container;
        private readonly T _prefab;

        public ObjectPool(T prefab)
        {
            _freeObjects = new List<IPoolAble>();
            _container = new GameObject().transform;
            _container.name = prefab.GameObject.name;
            _prefab = prefab;
        }

        public IPoolAble GetFreeObject()
        {
            IPoolAble poolAble;

            if (_freeObjects.Count > 0)
            {
                poolAble = _freeObjects[0] as T;
                _freeObjects.RemoveAt(0);
            }
            else
            {
                poolAble = Object.Instantiate(_prefab, _container);
            }

            if (poolAble == null)
            {
                throw new System.Exception("PoolAble object is null");
            }
            
            poolAble.GameObject.SetActive(true);
            poolAble.OnDestroyed += ReturnToPool;
        
            return poolAble;
        }

        private void ReturnToPool(IPoolAble poolAble)
        {
            _freeObjects.Add(poolAble);
            poolAble.OnDestroyed -= ReturnToPool;
            poolAble.GameObject.SetActive(false);
            poolAble.GameObject.transform.SetParent(_container);
        }
    }
}
