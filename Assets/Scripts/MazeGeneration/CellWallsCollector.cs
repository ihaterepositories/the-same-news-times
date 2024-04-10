using System;
using Controllers.InGameControllers;
using Pooling.Interfaces;
using UnityEngine;

namespace MazeGeneration
{
    public class CellWallsCollector : MonoBehaviour, IPoolAble
    {
        public GameObject leftWall;
        public GameObject bottomWall;

        public SpriteRenderer leftWallSpriteRenderer;
        public SpriteRenderer bottomWallSpriteRenderer;

        public GameObject GameObject => gameObject;
        public event Action<IPoolAble> OnDestroyed;

        private void OnEnable()
        {
            LevelFinisher.OnLevelFinished += Reset;
        }

        private void OnDisable()
        {
            LevelFinisher.OnLevelFinished -= Reset;
        }

        public void Reset()
        {
            MakeVisible();
            OnDestroyed?.Invoke(this);
        }
        
        private void MakeVisible()
        {
            bottomWallSpriteRenderer.color = new Color(
                bottomWallSpriteRenderer.color.r,
                bottomWallSpriteRenderer.color.g,
                bottomWallSpriteRenderer.color.b,
                1f);

            leftWallSpriteRenderer.color = new Color(
                bottomWallSpriteRenderer.color.r,
                bottomWallSpriteRenderer.color.g,
                bottomWallSpriteRenderer.color.b,
                1f);
        }
    }
}
