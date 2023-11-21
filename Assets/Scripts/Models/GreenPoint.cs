using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GreenPoint : MonoBehaviour, IEatable
{
    public static event Action OnEated;

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
        gameObject.SetActive(false);
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
