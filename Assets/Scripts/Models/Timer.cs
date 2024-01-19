using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private InfoText timerText;

    private float gameDuration;
    private Coroutine timerCoroutine;

    public float GameDuration { get { return gameDuration; } }

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

    public void StartTimer()
    {
        timerCoroutine = StartCoroutine(IncreaseTimeCoroutine());
    }

    public void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    private IEnumerator IncreaseTimeCoroutine()
    {
        while (true)
        {
            gameDuration += Time.deltaTime;
            timerText.SetText(TimeFormatter.Formate(gameDuration));
            yield return null;
        }
    }
}
