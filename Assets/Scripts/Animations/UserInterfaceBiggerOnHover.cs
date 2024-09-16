using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Animations
{
    public class UserInterfaceBiggerOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private RectTransform rectTransform;
        private Vector2 _originalScale;
        
        private void Awake()
        {
            _originalScale = rectTransform.localScale;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            rectTransform.DOScale(_originalScale * 1.25f, 0.5f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            rectTransform.DOScale(_originalScale, 0.5f);
        }
    }
}