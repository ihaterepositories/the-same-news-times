using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _duration;
    private float _timeRemaining;
    private bool isTimeOver;
    private Coroutine timerCoroutine;

    public float Duration { get { return _duration; } }
    public float TimeRemaining { get { return _timeRemaining; } }
    public bool IsTimeOver { get { return isTimeOver; } }

    public static event Action OnTimerStart;
    public static event Action<float> OnTimerWorking;
    public static event Action OnTimerFinish;

    private void Awake()
    {
        //_duration = PlayerPrefs.GetFloat("TimerSeconds", 300f);
        _duration = 10f;
    }

    private void OnEnable()
    {
        StartLevelController.OnAllSpawned += StartTimer;
        ExitObject.OnEated += StopTimer;
    }

    private void OnDisable()
    {
        StartLevelController.OnAllSpawned -= StartTimer;
        ExitObject.OnEated -= StopTimer;
    }

    public void StartTimer()
    {
        isTimeOver = false;
        _timeRemaining = _duration;
        OnTimerStart?.Invoke();
        timerCoroutine = StartCoroutine(StartTimerCoroutine());
    }

    public void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;

            PlayerPrefs.SetFloat("TimerSeconds", _timeRemaining);
        }
    }

    public void ContinueTimer()
    {
        timerCoroutine = StartCoroutine(StartTimerCoroutine());
    }

    private IEnumerator StartTimerCoroutine()
    {
        while (_timeRemaining >= 0f)
        {
            OnTimerWorking?.Invoke(_timeRemaining);
            _timeRemaining -= Time.deltaTime;
            yield return null;
        }

        TimeOver();
    }

    private void TimeOver()
    {
        isTimeOver = true;
        OnTimerFinish?.Invoke();
    }

    public bool IsMoreThanSomeTimeLeft(float seconds)
    {
        return _timeRemaining > seconds;
    }

    public bool IsLessThanSomeTimeLeft(float seconds)
    {
        return _timeRemaining < seconds;
    }
}
