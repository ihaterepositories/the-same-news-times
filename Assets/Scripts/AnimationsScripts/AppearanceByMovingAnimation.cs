using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]

public class AppearanceByMovingAnimation : MonoBehaviour
{
    [SerializeField] private float delayBeforeAnimationStart;
    [SerializeField] private Vector3 positionToMove;

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        yield return new WaitForSeconds(delayBeforeAnimationStart);
        rectTransform.DOLocalMove(positionToMove, 0.5f);
    }
}
