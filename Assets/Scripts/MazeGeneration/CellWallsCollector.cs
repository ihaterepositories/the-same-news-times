using System;
using Controllers;
using Controllers.InGameControllers;
using DG.Tweening;
using Interfaces;
using UnityEngine;

namespace Models.MazeGeneration
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
            OnDestroyed?.Invoke(this);
        }

        public void PlayAppearanceAnimation(float duration)
        {
            bottomWallSpriteRenderer.DOFade(1, duration);
            leftWallSpriteRenderer.DOFade(1, duration);
        }

        public void ChangeTransparency(float transparency)
        {
            bottomWallSpriteRenderer.color = new Color(
                bottomWallSpriteRenderer.color.r,
                bottomWallSpriteRenderer.color.g,
                bottomWallSpriteRenderer.color.b,
                transparency);

            leftWallSpriteRenderer.color = new Color(
                bottomWallSpriteRenderer.color.r,
                bottomWallSpriteRenderer.color.g,
                bottomWallSpriteRenderer.color.b,
                transparency);
        }
    }
}
