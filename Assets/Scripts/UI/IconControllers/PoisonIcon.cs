using DG.Tweening;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI.IconControllers
{
    public class PoisonIcon : MonoBehaviour
    {
        [SerializeField] private Image poisonIcon;

        private void Start()
        {
            HideIcon();
        }

        private void OnEnable()
        {
            Player.OnPoisoned += ShowIcon;
            Player.OnDePoisoned += HideIcon;
        }
        
        private void OnDisable()
        {
            Player.OnPoisoned -= ShowIcon;
            Player.OnDePoisoned -= HideIcon;
        }

        private void ShowIcon() => poisonIcon.DOFade(1f, 0.5f);
        
        private void HideIcon() => poisonIcon.DOFade(0.3f, 0.5f);
    }
}