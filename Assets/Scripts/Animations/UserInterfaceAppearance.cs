using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Animations
{
    [RequireComponent(typeof(RectTransform))]

    public class UserInterfaceAppearance : MonoBehaviour
    {
        [SerializeField] private float delay;
        [SerializeField] private Vector3 endPosition;
        [SerializeField] private RectTransform rectTransform;

        private void Start()
        {
            StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine()
        {
            yield return new WaitForSeconds(delay);
            rectTransform.DOLocalMove(endPosition, 0.5f);
        }
    }
}
