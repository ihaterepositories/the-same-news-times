using System;
using UnityEngine;

public class MenuElementsController : MonoBehaviour
{
    [SerializeField] private InfoText _bestScoresText;

    public static event Action OnMenuEntered;

    private void Start()
    {
        OnMenuEntered?.Invoke();
        SetBestScoresText();
    }

    private void SetBestScoresText()
    {
        _bestScoresText.SetText(
            $"{PlayerPrefs.GetInt("TotalMazesCompleted", 0)} mazes completed \n\n" +
            $"Best pink score: {PlayerPrefs.GetInt("BestPinkScore", 0)} \n" +
            $"Best green score: {PlayerPrefs.GetInt("BestGreenScore", 0)} \n" +
            $"Best total score: {PlayerPrefs.GetInt("BestTotalScore", 0)}"
            );
    }
}
