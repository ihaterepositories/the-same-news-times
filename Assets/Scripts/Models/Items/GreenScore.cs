using System;
using Controllers;
using Interfaces;
using UnityEngine;

namespace Models.Items
{
    public class GreenScore : MonoBehaviour, IPickAble, IPoolAble
    {
        public GameObject GameObject => gameObject;

        public static event Action OnPicked;
        public event Action<IPoolAble> OnDestroyed;

        private void OnEnable()
        {
            FinishLevelController.OnLevelFinished += Reset;
        }

        private void OnDisable()
        {
            FinishLevelController.OnLevelFinished -= Reset;
        }

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
