using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsMenuController : MonoBehaviour
{
    [SerializeField] private InfoText statisticsText;

    private void Start()
    {
        statisticsText.SetText(GetStatistics());
    }

    private string GetStatistics()
    {
        return $"{PlayerPrefs.GetInt("TotalMazesCompleted", 0)} mazes completed \n\n" +
               $"Best pink score: {PlayerPrefs.GetInt("BestPinkScore", 0)} \n" +
               $"Best green score: {PlayerPrefs.GetInt("BestGreenScore", 0)} \n" +
               $"Best total score: {PlayerPrefs.GetInt("BestTotalScore", 0)}";
    }
}
