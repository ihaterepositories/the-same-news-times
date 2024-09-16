using System;
using Controllers.InGameControllers;
using Models.Items.Interfaces;
using Pooling.Interfaces;
using UnityEngine;

namespace Models.Items
{
    public class Modificator : MonoBehaviour, IPoolAble, IPickAble
    {
        public GameObject GameObject => gameObject;

        public static event Action OnPicked;
        public event Action<IPoolAble> OnDestroyed;
        
        private void OnEnable()
        {
            LevelFinisher.OnLevelFinished += Reset;
        }

        private void OnDisable()
        {
            LevelFinisher.OnLevelFinished -= Reset;
        }

        public void Pick()
        {
            Debug.Log("Modificator picked");
            OnPicked?.Invoke();
            Reset();
        }
        
        public void Reset()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}