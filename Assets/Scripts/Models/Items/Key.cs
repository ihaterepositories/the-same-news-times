using System;
using Controllers;
using Interfaces;
using UI;
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
            Inventory.KeysCount++;
            OnPicked?.Invoke();
            Reset();
        }

        public void Reset()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}
