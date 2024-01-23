using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PickableItemOnStartAnimation : MonoBehaviour
{
    private Vector2 _standartScale;

    private void Awake()
    {
        _standartScale = transform.localScale;
    }

    private void OnEnable()
    {
        StartLevelController.OnAllSpawned += DoBreathAnimation;
    }

    private void OnDisable()
    {
        StartLevelController.OnAllSpawned -= DoBreathAnimation;
    }

    private void DoBreathAnimation()
    {
        StartCoroutine(BreatheAnimationCoroutine());
    }

    private IEnumerator BreatheAnimationCoroutine()
    {
        yield return new WaitForSeconds(3f);
        transform.DOScale(_standartScale + new Vector2(0.3f, 0.3f), 0.5f);
        yield return new WaitForSeconds(1f);
        transform.DOScale(_standartScale, 0.5f);
    }
}
