using System.Collections;
using Controllers;
using UI;
using UnityEngine;

namespace Models
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private InfoText timerText;

        private float _gameDuration;
        private Coroutine _timerCoroutine;

        public float GameDuration => _gameDuration;

        private void OnEnable()
        {
            StartLevelController.OnAllSpawned += StartTimer;
            FinishLevelController.OnLevelFinished += StopTimer;
            FinishLevelController.OnGameFinished += StopTimer;
        }

        private void OnDisable()
        {
            StartLevelController.OnAllSpawned -= StartTimer;
            FinishLevelController.OnLevelFinished -= StopTimer;
            FinishLevelController.OnGameFinished -= StopTimer;
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
