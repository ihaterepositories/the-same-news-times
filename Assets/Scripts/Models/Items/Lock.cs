using System;
using Controllers;
using Interfaces;
using UnityEngine;

namespace Models.Items
{
    public class Lock : MonoBehaviour, IPoolAble
    {
        public GameObject GameObject => gameObject;

        public event Action<IPoolAble> OnDestroyed;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<Player>() == null) return;
            Inventory.KeysCount--;
            Reset();
        }

        public void Reset()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}
