using System;
using System.Collections;
using UI.Formatters;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.InGameControllers
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private Text timerText;
        private float remainingTime;
        private bool isRunning;

        public static event Action OnTimerEnd;

        public void StartTimer(float duration)
        {
            if (!isRunning)
            {
                remainingTime = duration;
                UpdateTimerText();
                isRunning = true;
                StartCoroutine(TimerCoroutine());
            }
        }

        public void StopTimer()
        {
            isRunning = false;
            StopAllCoroutines();
        }

        private IEnumerator TimerCoroutine()
        {
            while (remainingTime > 0)
            {
                yield return new WaitForSeconds(1f);
                remainingTime--;
                UpdateTimerText();
            }

            isRunning = false;
            OnTimerEnd?.Invoke();
        }

        private void UpdateTimerText()
        {
            timerText.text = TimeFormatter.Format(remainingTime);
        }
        
    }
}
