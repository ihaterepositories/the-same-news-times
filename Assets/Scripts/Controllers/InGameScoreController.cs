using System;
using UnityEngine;

public class InGameScoreController : MonoBehaviour
{
    [SerializeField] private InfoText _greenScoreText;
    [SerializeField] private InfoText _pinkScoreText;
    [SerializeField] private Timer _gameDurationTimer;

    private int _greenScore = 0;
    private int _pinkScore = 0;
    private int _totalScore = 0;

    public static event Action OnPinkScoreUpdated;

    private void Start()
    {
        _greenScoreText.SetText(_greenScore);
        _pinkScoreText.SetText(_pinkScore);
    }

    private void OnEnable()
    {
        GreenScore.OnEated += UpdateGreenScore;
        PinkScore.OnEated += UpdatePinkScore;
    }

    private void OnDisable()
    {
        GreenScore.OnEated -= UpdateGreenScore;
        PinkScore.OnEated -= UpdatePinkScore;
    }

    private void UpdateGreenScore()
    {
        _greenScore++;
        _greenScoreText.SetText(_greenScore);
    }

    private void UpdatePinkScore()
    {
        _pinkScore++;
        _pinkScoreText.SetText(_pinkScore);
        PlayerPrefs.SetInt("TotalMazesCompleted", PlayerPrefs.GetInt("TotalMazesCompleted", 0) + 1);
        PlayerPrefs.Save();
        OnPinkScoreUpdated?.Invoke();
    }

    private string GetPinkScore()
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

    private string GetGreenScore()
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

    private string GetTotalScore()
    {
        _totalScore = (_pinkScore * _greenScore) + (int)_gameDurationTimer.GameDuration;

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

    private string GetGameDuration()
    {
        float currentGameDuration = _gameDurationTimer.GameDuration;
        float bestGameDuration = PlayerPrefs.GetFloat("BestGameDuration", 0f);

        string gameDurationString = $"game duration: {TimeFormatter.Formate(currentGameDuration)}";

        if (currentGameDuration > bestGameDuration)
        {
            PlayerPrefs.SetFloat("BestGameDuration", currentGameDuration);
            gameDurationString = $"new longest game record: {TimeFormatter.Formate(currentGameDuration)} !";
        }

        return gameDurationString;
    }

    public string GetCurrentGameScore()
    {
        return GetPinkScore() + "\n" + "\n" +
               GetGreenScore() + "\n" + "\n" +
               GetTotalScore() + "\n" + "\n" +
               GetGameDuration();
    }
}
