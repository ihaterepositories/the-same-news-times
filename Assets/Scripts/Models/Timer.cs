using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private InfoText _timerText;
    [SerializeField] private Color _timeGoesOutColor;

    private float _duration;
    private float _timeRemaining;
    private Coroutine timerCoroutine;

    public float TimeRemaining { get { return _timeRemaining; } set { _timeRemaining = value; } }

    public static event Action OnTimerStart;
    public static event Action<float> OnTimerWorking;
    public static event Action OnTimerFinish;

    private void Awake()
    {
        _duration = PlayerPrefs.GetFloat("TimerSeconds", 300f);
    }

    private void Update()
    {
        StopTimerByPlayer();
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

    private IEnumerator StartTimerCoroutine()
    {
        while (_timeRemaining >= 0f)
        {
            OnTimerWorking?.Invoke(_timeRemaining);
            SetTimerText(_timeRemaining);
            _timeRemaining -= Time.deltaTime;
            yield return null;
        }

        OnTimerFinish?.Invoke();
    }

    private void SetTimerText(float timeRemaining)
    {
        if (timeRemaining <= 10f)
        {
            _timerText.TextObject.color = _timeGoesOutColor;
        }

        TimeSpan t = TimeSpan.FromSeconds(timeRemaining);
        _timerText.SetText(string.Format("{0:00}:{1:00}:{2:00}", ((int)t.TotalHours), t.Minutes, t.Seconds));
    }

    private void StopTimerByPlayer()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            _timeRemaining = 0f;
        }
    }
}
