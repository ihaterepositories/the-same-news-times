using System;
using Models.Items.Interfaces;
using Pooling.Interfaces;
using UnityEngine;

namespace Models.Items
{
    public class Key : MonoBehaviour, IPickAble, IPoolAble
    {
        public GameObject GameObject => gameObject;

        public event Action<IPoolAble> OnDestroyed;
        public static event Action OnPicked;

        public void Pick()
        {
            OnPicked?.Invoke();
            Reset();
        }

        public void Reset()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}
