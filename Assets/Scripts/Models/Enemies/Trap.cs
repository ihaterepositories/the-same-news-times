using DG.Tweening;
using System;
using System.Collections;
using Models;
using UnityEngine;
using Random = UnityEngine.Random;

public class Trap : MonoBehaviour, IPoolable
{
    [SerializeField] private Collider2D _collider;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Coroutine _appearingCoroutine;

    public GameObject GameObject => gameObject;

    public static event Action OnCaughtPlayer;
    public event Action<IPoolable> OnDestroyed;

    private void OnEnable()
    {
        FinishLevelController.OnLevelFinished += Reset;
    }

    private void OnDisable()
    {
        FinishLevelController.OnLevelFinished -= Reset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            OnCaughtPlayer?.Invoke();
            player.Reset();
        }
    }

    public void Reset()
    {
        StopAppearingAnimation();
        OnDestroyed?.Invoke(this);
    }

    private void StopAppearingAnimation()
    {
        if (_appearingCoroutine != null)
        {
            StopCoroutine(_appearingCoroutine);
            _appearingCoroutine = null;
        }
    }

    public void PlayAppearingAnimation()
    {
        _appearingCoroutine = StartCoroutine(AppearingAnimationCoroutine());
    }

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
        _collider.enabled = isActive;
        _spriteRenderer.DOFade(isActive ? 1 : 0, 0.5f);
    }
}
