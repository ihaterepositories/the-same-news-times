using System;
using Interfaces;
using UnityEngine;

namespace Models.Items
{
    public class Lock : MonoBehaviour, IPoolAble
    {
        public GameObject GameObject => gameObject;

        public event Action<IPoolAble> OnDestroyed;
        public static event Action<Lock> OnUnlockTry;
        public static event Action OnUnlocked;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<Player>() == null) return;
            OnUnlockTry?.Invoke(this);
        }

        public void Reset()
        {
            OnUnlocked?.Invoke();
            OnDestroyed?.Invoke(this);
        }
    }
}
