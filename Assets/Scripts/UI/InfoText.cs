using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]

public class InfoText : MonoBehaviour
{
    [SerializeField] private bool isColorChangable;
    private Text _textObject;

    public Text TextObject { get { return _textObject; } }

    private void Awake()
    {
        _textObject = GetComponent<Text>();
    }

    private void OnEnable()
    {
        TempleKeeper.OnPlayerInDangeroues += SetLightColor;
        TempleKeeper.OnEndOfPlayerDangeroues += SetDarkColor;
    }

    private void OnDisable()
    {
        TempleKeeper.OnPlayerInDangeroues -= SetLightColor;
        TempleKeeper.OnEndOfPlayerDangeroues -= SetDarkColor;
    }

    private void SetLightColor()
    {
        if (isColorChangable)
        _textObject.DOColor(new Color(0.9490196f, 0.9607843f, 0.9176471f), 1f);
    }

    private void SetDarkColor()
    {
        if (isColorChangable)
        _textObject.DOColor(new Color(0.1647059f, 0.2078431f, 0.2509804f), 1f);
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
