using System;
using DG.Tweening;
using Models.Items;
using UI.Formatters;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Controllers.InGameControllers
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private Text mazesCompletedText;
        [SerializeField] private Text scoreText;
        
        private int _score;
        private int _mazesCompleted;
        private int _pointsPicked;

        public static event Action OnMazesScoreUpdated;

        private void OnEnable()
        {
            Point.OnPicked += UpdatePickedPointsCount;
            MazeExit.OnPicked += UpdateCompletedMazesCount;
        }

        private void OnDisable()
        {
            Point.OnPicked -= UpdatePickedPointsCount;
            MazeExit.OnPicked -= UpdateCompletedMazesCount;
        }
        
        public string GetCurrentGameScore()
        {
            string score = "earned " + ScoresFormatter.FormatNumber(_score) + "score";
            var bestScore = PlayerPrefs.GetInt("BestScore", 0);
            
            if (_score > bestScore)
            {
                PlayerPrefs.SetInt("BestScore", _score);
                PlayerPrefs.Save();
                score += " !";
            }
            
            return score;
        }

        private void UpdatePickedPointsCount()
        {
            _pointsPicked += 100;
            UpdateScore();
        }
        
        private void UpdateCompletedMazesCount()
        {
            _mazesCompleted++;
            UpdateScore();
            FlipText(mazesCompletedText);
            mazesCompletedText.text = ScoresFormatter.FormatNumber(_mazesCompleted);
            PlayerPrefs.SetInt("TotalMazesCompleted", PlayerPrefs.GetInt("TotalMazesCompleted", 0) + 1);
            PlayerPrefs.Save();
            OnMazesScoreUpdated?.Invoke();
        }

        private void UpdateScore()
        {
            if (_mazesCompleted == 0)
            {
                _score = _pointsPicked;
            }
            else
            {
                _score = _mazesCompleted * _pointsPicked;
            }
            
            FlipText(scoreText);
            scoreText.text = "score: " + ScoresFormatter.FormatNumber(_score);
        }

        private void FlipText(Text textObject)
        {
            textObject.rectTransform.DORotate(new Vector3(360f, 0f, 0f), 0.5f, RotateMode.FastBeyond360);
        }
    }
}
