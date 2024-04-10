using System;
using Controllers.InGameControllers;
using Models.Items.Interfaces;
using Pooling.Interfaces;
using UnityEngine;

namespace Models.Items
{
    public class Poison : MonoBehaviour, IPickAble, IPoolAble
    {
        public GameObject GameObject => gameObject;
        
        public event Action<IPoolAble> OnDestroyed;
        public static event Action OnPicked;
        
        private void OnEnable()
        {
            LevelFinisher.OnLevelFinished += Reset;
            LevelFinisher.OnGameFinished += Reset;
        }

        private void OnDisable()
        {
            LevelFinisher.OnLevelFinished -= Reset;
            LevelFinisher.OnGameFinished -= Reset;
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