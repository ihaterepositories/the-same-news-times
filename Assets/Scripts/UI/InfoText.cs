using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class InfoText : MonoBehaviour
{
    private Text _textObject;

    public Text TextObject { get { return _textObject; } }

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

    public void SetText(float number)
    {
        _textObject.text = number.ToString();
    }

    public void SetText(double number)
    {
        _textObject.text = number.ToString();
    }
}
