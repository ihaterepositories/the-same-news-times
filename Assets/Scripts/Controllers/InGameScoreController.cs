using System;
using Models;
using Models.Items;
using UI;
using UnityEngine;

namespace Controllers
{
    public class InGameScoreController : MonoBehaviour
    {
        [SerializeField] private InfoText greenScoreText;
        [SerializeField] private InfoText pinkScoreText;
        [SerializeField] private Timer gameDurationTimer;

        private int _greenScore;
        private int _pinkScore;
        private int _totalScore;

        public static event Action OnPinkScoreUpdated;

        private void Start()
        {
            greenScoreText.SetText(_greenScore);
            pinkScoreText.SetText(_pinkScore);
        }

        private void OnEnable()
        {
            GreenScore.OnPicked += UpdateGreenScore;
            PinkScore.OnPicked += UpdatePinkScore;
        }

        private void OnDisable()
        {
            GreenScore.OnPicked -= UpdateGreenScore;
            PinkScore.OnPicked -= UpdatePinkScore;
        }

        private void UpdateGreenScore()
        {
            _greenScore++;
            greenScoreText.SetText(_greenScore);
        }

        private void UpdatePinkScore()
        {
            _pinkScore++;
            pinkScoreText.SetText(_pinkScore);
            PlayerPrefs.SetInt("TotalMazesCompleted", PlayerPrefs.GetInt("TotalMazesCompleted", 0) + 1);
            PlayerPrefs.Save();
            OnPinkScoreUpdated?.Invoke();
        }

        private string GetPinkScore()
        {
            var bestPinkScore = PlayerPrefs.GetInt("BestPinkScore", 0);
            var scoreString = $"pink score: {_pinkScore}";

            if (_pinkScore <= bestPinkScore) return scoreString;
            
            PlayerPrefs.SetInt("BestPinkScore", _pinkScore);
            PlayerPrefs.Save();
            scoreString = $"new best pink score: {_pinkScore} !";
            return scoreString;
        }

        private string GetGreenScore()
        {
            var bestGreenScore = PlayerPrefs.GetInt("BestGreenScore", 0);
            var scoreString = $"green score: {_greenScore}";

            if (_greenScore <= bestGreenScore) return scoreString;
            
            PlayerPrefs.SetInt("BestGreenScore", _greenScore);
            PlayerPrefs.Save();
            scoreString = $"new best green score: {_greenScore} !";
            return scoreString;
        }

        private string GetTotalScore()
        {
            _totalScore = (_pinkScore * _greenScore) + (int)gameDurationTimer.GameDuration;

            var bestTotalScore = PlayerPrefs.GetInt("BestTotalScore", 0);
            var scoreString = $"total score: {_totalScore}";

            if (_totalScore <= bestTotalScore) return scoreString;
            
            PlayerPrefs.SetInt("BestTotalScore", _totalScore);
            PlayerPrefs.Save();
            scoreString = $"new best total score: {_totalScore} !";
            return scoreString;
        }

        private string GetGameDuration()
        {
            var currentGameDuration = gameDurationTimer.GameDuration;
            var bestGameDuration = PlayerPrefs.GetFloat("BestGameDuration", 0f);

            var gameDurationString = $"game duration: {TimeFormatter.Format(currentGameDuration)}";

            if (!(currentGameDuration > bestGameDuration)) return gameDurationString;
            
            PlayerPrefs.SetFloat("BestGameDuration", currentGameDuration);
            gameDurationString = $"new longest game record: {TimeFormatter.Format(currentGameDuration)} !";
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
}
