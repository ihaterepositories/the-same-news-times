using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class GreenScore : MonoBehaviour, IEatable, IPoolable
{
    public GameObject GameObject => gameObject;

    public static event Action OnEated;
    public event Action<IPoolable> OnDestroyed;

    private void OnEnable()
    {
        StartLevelController.OnAllSpawned += DoBreathAnimation;
        FinishLevelController.OnLevelFinished += Reset;
    }

    private void OnDisable()
    {
        StartLevelController.OnAllSpawned -= DoBreathAnimation;
        FinishLevelController.OnLevelFinished -= Reset;
    }

    public void Eated()
    {
        OnEated?.Invoke();
        Reset();
    }

    public void Reset()
    {
        OnDestroyed?.Invoke(this);
    }

    private void DoBreathAnimation()
    {
        StartCoroutine(BreatheAnimationCoroutine());
    }

    private IEnumerator BreatheAnimationCoroutine()
    {
        yield return new WaitForSeconds(3f);
        transform.DOScaleX(0.6f, 0.5f);
        transform.DOScaleY(0.6f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        transform.DOScaleX(0.3f, 0.5f);
        transform.DOScaleY(0.3f, 0.5f);
    }
}
