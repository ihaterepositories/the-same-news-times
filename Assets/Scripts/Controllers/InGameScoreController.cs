using System;
using UnityEngine;

public class InGameScoreController : MonoBehaviour
{
    [SerializeField] private InfoText _greenScoreText;
    [SerializeField] private InfoText _pinkScoreText;

    private int _greenScore;
    private int _pinkScore;
    private int _totalScore;

    public static event Action OnPinkScoreUpdated;

    private void Start()
    {
        _greenScore = PlayerPrefs.GetInt("GreenScore", 0);
        _pinkScore = PlayerPrefs.GetInt("PinkScore", 0);

        _greenScoreText.SetText(_greenScore);
        _pinkScoreText.SetText(_pinkScore);
    }

    private void OnEnable()
    {
        GreenPoint.OnEated += UpdateGreenScore;
        ExitObject.OnEated += UpdatePinkScore;
    }

    private void OnDisable()
    {
        GreenPoint.OnEated -= UpdateGreenScore;
        ExitObject.OnEated -= UpdatePinkScore;
    }

    private void UpdateGreenScore()
    {
        _greenScore++;
        _greenScoreText.SetText(_greenScore);

        PlayerPrefs.SetInt("GreenScore", _greenScore);
        PlayerPrefs.Save();
    }

    private void UpdatePinkScore()
    {
        _pinkScore++;
        _pinkScoreText.SetText(_pinkScore);

        PlayerPrefs.SetInt("PinkScore", _pinkScore);
        PlayerPrefs.SetInt("TotalMazesCompleted", PlayerPrefs.GetInt("TotalMazesCompleted", 0) + 1);
        PlayerPrefs.Save();

        OnPinkScoreUpdated?.Invoke();
    }

    private string CalculatePinkScore()
    {
        int bestPinkScore = PlayerPrefs.GetInt("BestPinkScore", 0);
        string scoreString = $"pink score: {_pinkScore}";

        if (_pinkScore > bestPinkScore)
        {
            PlayerPrefs.SetInt("BestPinkScore", _pinkScore);
            PlayerPrefs.Save();
            scoreString = $"new best pink score: {_pinkScore} !";
        }

        return scoreString;
    }

    private string CalculateGreenScore()
    {
        int bestGreenScore = PlayerPrefs.GetInt("BestGreenScore", 0);
        string scoreString = $"green score: {_greenScore}";

        if (_greenScore > bestGreenScore)
        {
            PlayerPrefs.SetInt("BestGreenScore", _greenScore);
            PlayerPrefs.Save();
            scoreString = $"new best green score: {_greenScore} !";
        }

        return scoreString;
    }

    private string CalculateTotalScore()
    {
        _totalScore = _pinkScore * _greenScore;

        int bestTotalScore = PlayerPrefs.GetInt("BestTotalScore", 0);
        string scoreString = $"total score: {_totalScore}";

        if (_totalScore > bestTotalScore)
        {
            PlayerPrefs.SetInt("BestTotalScore", _totalScore);
            PlayerPrefs.Save();
            scoreString = $"new best total score: {_totalScore} !";
        }

        return scoreString;
    }

    public string GetScoresString()
    {
        return CalculatePinkScore() + "\n" + "\n" +
               CalculateGreenScore() + "\n" + "\n" +
               CalculateTotalScore();
    }
}
