using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private ScoreText _greenScoreText;
    [SerializeField] private ScoreText _pinkScoreText;

    private int _greenScore;
    private int _pinkScore;

    public static event Action OnPinkScoreUpdated;

    private void Start()
    {
        _greenScore = PlayerPrefs.GetInt("GreenScore", 0);
        _pinkScore = PlayerPrefs.GetInt("PinkScore", 0);

        UpdateGreenScoreText();
        UpdatePinkScoreText();
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
        UpdateGreenScoreText();
        PlayerPrefs.SetInt("GreenScore", _greenScore);
    }

    private void UpdatePinkScore()
    {
        _pinkScore++;
        UpdatePinkScoreText();
        PlayerPrefs.SetInt("PinkScore", _pinkScore);

        int totalMazesCompleted = PlayerPrefs.GetInt("TotalMazesCompleted", 0) + 1;
        PlayerPrefs.SetInt("TotalMazesCompleted", totalMazesCompleted);

        OnPinkScoreUpdated?.Invoke();
    }

    private void UpdateGreenScoreText()
    {
        _greenScoreText.UpdateScore(_greenScore);
    }

    private void UpdatePinkScoreText()
    {
        _pinkScoreText.UpdateScore(_pinkScore);
    }
}
