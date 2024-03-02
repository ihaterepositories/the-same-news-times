using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
    public class ColorfulText : MonoBehaviour
    {
        [SerializeField] private Text textComponent;
        [SerializeField] private string text;
        [SerializeField] private Color[] colors;

        private void Start()
        {
            textComponent.text = SetColorfulText();
        }

        private string SetColorfulText() => 
            text.Aggregate("", (current, t) => current + ("<color=#" + ColorUtility.ToHtmlStringRGB(colors[Random.Range(0, colors.Length)]) + ">" + t + "</color>"));
        
    }
}
