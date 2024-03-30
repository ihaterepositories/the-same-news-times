using DG.Tweening;
using Models.Enemies;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ColorChangeableText : MonoBehaviour
    {
        [SerializeField] private Text textObject;
        
        private void OnEnable()
        {
            TempleKeeper.OnPlayerInDangerous += SetLightColor;
            TempleKeeper.OnEndOfPlayerDangerous += SetDarkColor;
        }

        private void OnDisable()
        {
            TempleKeeper.OnPlayerInDangerous -= SetLightColor;
            TempleKeeper.OnEndOfPlayerDangerous -= SetDarkColor;
        }

        private void SetLightColor()
        {
            textObject.DOColor(new Color(0.9490196f, 0.9607843f, 0.9176471f), 1f);
        }

        private void SetDarkColor()
        {
            textObject.DOColor(new Color(0.1647059f, 0.2078431f, 0.2509804f), 1f);
        }
    }
}