using Controllers.InGameControllers;
using DG.Tweening;
using Models.Items;
using UnityEngine;
using UnityEngine.UI;

namespace UI.IconControllers
{
    public class KeyIcon : MonoBehaviour
    {
        [SerializeField] private Image keyIcon;

        private void Start()
        {
            HideIcon();
        }
        
        private void OnEnable()
        {
            Key.OnPicked += ShowIcon;
            Inventory.OnKeyUsed += HideIcon;
        }
        
        private void OnDisable()
        {
            Key.OnPicked -= ShowIcon;
            Inventory.OnKeyUsed -= HideIcon;
        }

        private void ShowIcon() => keyIcon.DOFade(1f, 0.5f);
        
        private void HideIcon() => keyIcon.DOFade(0.3f, 0.5f);
    }
}