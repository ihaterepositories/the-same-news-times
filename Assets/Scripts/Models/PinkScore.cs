using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class PinkScore : MonoBehaviour, IEatable, IPoolable
{
    public GameObject GameObject => gameObject;

    public static event Action OnEated;
    public event Action<IPoolable> OnDestroyed;

    private void OnEnable()
    {
        StartLevelController.OnAllSpawned += DoBreathAnimation;
    }

    private void OnDisable()
    {
        StartLevelController.OnAllSpawned -= DoBreathAnimation;
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
        yield return new WaitForSeconds(1f);
        transform.DOScaleX(0.3f, 0.5f);
        transform.DOScaleY(0.3f, 0.5f);
    }
}
