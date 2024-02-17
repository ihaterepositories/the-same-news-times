using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Models.Enemies
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]

    public class TempleKeeperTriggerZone : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D triggerCollider;
        
        private SpriteRenderer _spriteRenderer;
        private Coroutine _breathingCoroutine;
        private bool _isBreathing;

        public event Action OnTriggerEnter;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<Player>() == null) return;
            StopBreathing();
            triggerCollider.enabled = false;
            OnTriggerEnter?.Invoke();
        }

        private void Increase()
        {
            transform.DOScale(new Vector2(80f, 80f), 1f);
        }

        public void SetAlphaOfColor(float alpha)
        {
            _spriteRenderer.DOFade(alpha, 0.7f);
        }

        public void StartBreathing()
        {
            _isBreathing = true;
            triggerCollider.enabled = true;
            transform.localScale = Vector2.zero;
            _breathingCoroutine = StartCoroutine(DoBreathingCoroutine());
        }

        private IEnumerator DoBreathingCoroutine()
        {
            while (_isBreathing)
            {
                if (_isBreathing)
                {
                    transform.DOScale(4.5f, 1.5f);
                    yield return new WaitForSeconds(1.5f);
                    
                    if (!_isBreathing) continue;
                    transform.DOScale(0f, 1f);
                    yield return new WaitForSeconds(1f);
                }
                else yield break;
            }
        }

        private void StopBreathing()
        {
            _isBreathing = false;
            if (_breathingCoroutine == null) return;
            StopCoroutine(_breathingCoroutine);
            _breathingCoroutine = null;
            Increase();
        }
    }
}
