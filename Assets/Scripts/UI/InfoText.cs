using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Text))]

    public class InfoText : MonoBehaviour
    {
        private Text _textObject;

        private void Awake()
        {
            _textObject = GetComponent<Text>();
        }

        public void SetText(string text)
        {
            _textObject.text = text;
        }

        public void SetText(int number)
        {
            _textObject.text = number.ToString();
        }

        public void SetColor(Color color)
        {
            _textObject.color = color;
        }
    }
}
