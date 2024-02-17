using System;
using Interfaces;
using UnityEngine;

namespace Models.Items
{
    public class Lock : MonoBehaviour, IPoolAble
    {
        public GameObject GameObject => gameObject;

        public event Action<IPoolAble> OnDestroyed;

        private void OnEnable()
        {
            Key.OnPicked += Reset;
        }

        private void OnDisable()
        {
            Key.OnPicked -= Reset;
        }

        public void Reset()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}
