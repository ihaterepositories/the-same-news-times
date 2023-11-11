using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TotalMazesCompletedText : MonoBehaviour
{
    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
        ShowText();
    }

    private void ShowText()
    {
        int totalMazesCompleted = PlayerPrefs.GetInt("TotalMazesCompleted", 0);
        _text.text = $"{totalMazesCompleted} mazes completed";
    }
}
