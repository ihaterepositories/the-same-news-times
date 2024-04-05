using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Animations
{
    [RequireComponent(typeof(RectTransform))]

    public class AppearanceFromOutsideTheScreen : MonoBehaviour
    {
        [SerializeField] private float delayBeforeAnimationStart;
        [SerializeField] private Vector3 positionToMove;

        private RectTransform _rectTransform;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine()
        {
            yield return new WaitForSeconds(delayBeforeAnimationStart);
            _rectTransform.DOLocalMove(positionToMove, 0.5f);
        }
    }
}
