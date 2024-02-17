using System;
using Interfaces;
using UnityEngine;

namespace Models.Items
{
    public class PinkScore : MonoBehaviour, IPickAble, IPoolAble
    {
        public GameObject GameObject => gameObject;

        public static event Action OnPicked;
        public event Action<IPoolAble> OnDestroyed;

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
