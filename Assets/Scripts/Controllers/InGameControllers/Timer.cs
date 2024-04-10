using System.Collections;
using UI.Formatters;
using UI.TextControllers;
using UnityEngine;

namespace Controllers.InGameControllers
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private InfoText timerText;

        private float _gameDuration;
        private Coroutine _timerCoroutine;

        public float GameDuration => _gameDuration;

        private void OnEnable()
        {
            LevelStarter.OnAllSpawned += StartTimer;
            LevelFinisher.OnLevelFinished += StopTimer;
            LevelFinisher.OnGameFinished += StopTimer;
        }

        private void OnDisable()
        {
            LevelStarter.OnAllSpawned -= StartTimer;
            LevelFinisher.OnLevelFinished -= StopTimer;
            LevelFinisher.OnGameFinished -= StopTimer;
        }

        private void StartTimer()
        {
            _timerCoroutine = StartCoroutine(IncreaseTimeCoroutine());
        }

        private void StopTimer()
        {
            if (_timerCoroutine == null) return;
            StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;
        }

        private IEnumerator IncreaseTimeCoroutine()
        {
            while (true)
            {
                _gameDuration += Time.deltaTime;
                timerText.SetText(TimeFormatter.Format(_gameDuration));
                yield return null;
            }
        }
    }
}
