using System;
using DataModels;
using Enums;
using Models.Items;
using Requests;
using UI;
using UnityEngine;
using Zenject;

namespace Controllers.InGameControllers
{
    public class ScoresCounter : MonoBehaviour
    {
        [SerializeField] private InfoText greenScoreText;
        [SerializeField] private InfoText pinkScoreText;
        [SerializeField] private Timer gameDurationTimer;
        [SerializeField] private InfoText updateOnlineBestRecordRequestResultText;

        private int _greenScore;
        private int _pinkScore;
        private int _totalScore;
        private bool _isNewBestRecord;
        private UpdateBestRecordRequest _updateBestRecordRequest;
        private ScoresFormatter _scoresFormatter;

        public static event Action OnPinkScoreUpdated;

        [Inject]
        private void Construct(UpdateBestRecordRequest updateBestRecordRequest, ScoresFormatter scoresFormatter)
        {
            _updateBestRecordRequest = updateBestRecordRequest;
            _scoresFormatter = scoresFormatter;
        }

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
        
        public string GetCurrentGameScore()
        {
            return CalculatePinkScoreRecord() + "\n" + "\n" +
                   CalculateGreenScoreRecord() + "\n" + "\n" +
                   CalculateTotalScoreRecord() + "\n" + "\n" +
                   CalculateGameDurationRecord();
        }

        private string CalculatePinkScoreRecord()
        {
            var bestPinkScore = PlayerPrefs.GetInt("BestPinkScore", 0);
            var scoreString = $"pink score: {_scoresFormatter.FormatNumber(_pinkScore)}";

            if (_pinkScore <= bestPinkScore) return scoreString;
            
            PlayerPrefs.SetInt("BestPinkScore", _pinkScore);
            PlayerPrefs.Save();
            scoreString = $"new best pink score: {_scoresFormatter.FormatNumber(_pinkScore)} !";
            _isNewBestRecord = true;
            return scoreString;
        }

        private string CalculateGreenScoreRecord()
        {
            var bestGreenScore = PlayerPrefs.GetInt("BestGreenScore", 0);
            var scoreString = $"green score: {_scoresFormatter.FormatNumber(_greenScore)}";

            if (_greenScore <= bestGreenScore) return scoreString;
            
            PlayerPrefs.SetInt("BestGreenScore", _greenScore);
            PlayerPrefs.Save();
            scoreString = $"new best green score: {_scoresFormatter.FormatNumber(_greenScore)} !";
            _isNewBestRecord = true;
            return scoreString;
        }

        private string CalculateTotalScoreRecord()
        {
            _totalScore = (_pinkScore * _greenScore) + (int)gameDurationTimer.GameDuration;

            var bestTotalScore = PlayerPrefs.GetInt("BestTotalScore", 0);
            var scoreString = $"total score: {_scoresFormatter.FormatNumber(_totalScore)}";

            if (_totalScore <= bestTotalScore) return scoreString;
            
            PlayerPrefs.SetInt("BestTotalScore", _totalScore);
            PlayerPrefs.Save();
            scoreString = $"new best total score: {_scoresFormatter.FormatNumber(_totalScore)} !";
            _isNewBestRecord = true;
            return scoreString;
        }

        private string CalculateGameDurationRecord()
        {
            var currentGameDuration = gameDurationTimer.GameDuration;
            var bestGameDuration = PlayerPrefs.GetFloat("BestGameDuration", 0f);

            var gameDurationString = $"game duration: {TimeFormatter.Format(currentGameDuration)}";

            if (!(currentGameDuration > bestGameDuration)) return gameDurationString;
            
            PlayerPrefs.SetFloat("BestGameDuration", currentGameDuration);
            gameDurationString = $"new longest game record: {TimeFormatter.Format(currentGameDuration)} !";
            return gameDurationString;
        }
        
        private void UpdateGreenScore()
        {
            _greenScore++;
            greenScoreText.SetText(_scoresFormatter.FormatNumber(_greenScore));
        }

        private void UpdatePinkScore()
        {
            _pinkScore++;
            pinkScoreText.SetText(_scoresFormatter.FormatNumber(_pinkScore));
            PlayerPrefs.SetInt("TotalMazesCompleted", PlayerPrefs.GetInt("TotalMazesCompleted", 0) + 1);
            PlayerPrefs.Save();
            OnPinkScoreUpdated?.Invoke();
        }

        public void CalculateTotalTimeSpent()
        {
            PlayerPrefs.SetFloat("TotalTimeSpent", PlayerPrefs.GetFloat("TotalTimeSpent", 0f) + gameDurationTimer.GameDuration);
        }
        
        public void UpdateOnlineBestScore()
        {
            var bestRecordData = new BestRecordData
            {
                id = PlayerPrefs.GetString("bestRecordId"),
                totalScore = _totalScore.ToString(),
                pinkScore = _pinkScore.ToString(),
                greenScore = _greenScore.ToString(),
                playerId = PlayerPrefs.GetString("playerId")
            };
            
            if (_isNewBestRecord)
            {
                StartCoroutine(_updateBestRecordRequest.UpdateRecord(bestRecordData, ProcessUpdateBestRecordRequestResult));
            }
        }

        private void ProcessUpdateBestRecordRequestResult(StatusCode statusCode, string requestResultText)
        {
            if (statusCode == StatusCode.Success)
            {
                updateOnlineBestRecordRequestResultText.SetText("Online best record updated for: " + PlayerPrefs.GetString("username"));
            }
        }
    }
}
