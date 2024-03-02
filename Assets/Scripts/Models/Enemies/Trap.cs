using System;
using System.Collections;
using Controllers;
using DG.Tweening;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Models.Enemies
{
    public class Trap : MonoBehaviour, IPoolAble
    {
        [SerializeField] private Collider2D trapCollider;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Coroutine _appearingCoroutine;

        public GameObject GameObject => gameObject;

        public static event Action OnCaughtPlayer;
        public event Action<IPoolAble> OnDestroyed;

        private void OnEnable()
        {
            FinishLevelController.OnLevelFinished += Reset;
            FinishLevelController.OnGameFinished += Reset;
        }

        private void OnDisable()
        {
            FinishLevelController.OnLevelFinished -= Reset;
            FinishLevelController.OnGameFinished -= Reset;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var player = collision.gameObject.GetComponent<Player>();

            if (player == null) return;
            OnCaughtPlayer?.Invoke();
            player.Reset();
        }

        public void Reset()
        {
            StopAppearingAnimation();
            OnDestroyed?.Invoke(this);
        }
        
        private void StopAppearingAnimation()
        {
            if (_appearingCoroutine == null) return;
            StopCoroutine(_appearingCoroutine);
            _appearingCoroutine = null;
        }

        public void PlayAppearingAnimation()
        {
            _appearingCoroutine = StartCoroutine(AppearingAnimationCoroutine());
        }

        // ReSharper disable once FunctionRecursiveOnAllPaths
        private IEnumerator AppearingAnimationCoroutine()
        {
            yield return new WaitForSeconds(Random.Range(1.5f, 3f));
            MakeActive(false);
            yield return new WaitForSeconds(Random.Range(1.5f, 3f));
            MakeActive(true);
            StartCoroutine(AppearingAnimationCoroutine());
        }

        private void MakeActive(bool isActive)
        {
            trapCollider.enabled = isActive;
            spriteRenderer.DOFade(isActive ? 1 : 0, 0.5f);
        }
    }
}
