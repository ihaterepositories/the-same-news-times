using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class BestScoresText : MonoBehaviour
{
    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
        ShowText();
    }

    private void ShowText()
    {
        _text.text =
            $"{PlayerPrefs.GetInt("TotalMazesCompleted", 0)} mazes completed \n\n" +
            $"Best pink score: {PlayerPrefs.GetInt("BestPinkScore", 0)} \n" +
            $"Best green score: {PlayerPrefs.GetInt("BestGreenScore", 0)} \n" +
            $"Best total score: {PlayerPrefs.GetInt("BestTotalScore", 0)}";
    }
}
