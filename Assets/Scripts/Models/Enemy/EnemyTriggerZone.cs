using DG.Tweening;
using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class EnemyTriggerZone : MonoBehaviour
{
    [SerializeField] private CircleCollider2D triggerCollider;

    private SpriteRenderer spriteRenderer;
    private Sequence sequence;

    public event Action OnTriggerEnter;

    private void Start()
    {
        triggerCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        sequence = DOTween.Sequence();

        DoBreathing();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            sequence.Kill();
            triggerCollider.enabled = false;
            Increase();
            OnTriggerEnter?.Invoke();
        }
    }

    private void Increase()
    {
        transform.localScale = Vector2.zero;
        transform.DOScale(new Vector2(80f, 80f), 1f);
    }

    public void SetAlphaOfColor(float alpha)
    {
        spriteRenderer.DOFade(alpha, 0.7f);
    }

    private void DoBreathing()
    {
        sequence.Append(transform.DOScale(4.5f, 1.5f));
        sequence.Append(transform.DOScale(1.5f, 1f));

        sequence.SetLoops(-1);
    }
}
