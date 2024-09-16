using DG.Tweening;
using UnityEngine;

namespace Animations
{
    public class UserInterfaceBlinkingAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        private Vector2 _originalScale;
        
        private void Awake()
        {
            _originalScale = rectTransform.localScale;
        }
        
        private void OnEnable()
        {
            rectTransform.DOScale(_originalScale * 0.95f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }
    }
}