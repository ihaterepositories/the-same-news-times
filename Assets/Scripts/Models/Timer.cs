using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private InfoText timerText;

    private float gameDuration;
    private Coroutine timerCoroutine;

    private void Awake()
    {
        gameDuration = PlayerPrefs.GetFloat("GameDuration", 0f);
    }

    private void Start()
    {
        StartTimer();
    }

    private void OnEnable()
    {
        FinishLevelController.OnLevelFinished += StopTimer;
        FinishLevelController.OnGameFinished += StopTimer;
    }

    private void OnDisable()
    {
        FinishLevelController.OnLevelFinished -= StopTimer;
        FinishLevelController.OnGameFinished -= StopTimer;
    }

    private void StartTimer()
    {
        Debug.Log("Timer started");
        timerCoroutine = StartCoroutine(IncreaseTimeCoroutine());
    }

    public void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
            PlayerPrefs.SetFloat("GameDuration", gameDuration);
        }
    }

    private IEnumerator IncreaseTimeCoroutine()
    {
        while (true)
        {
            gameDuration += Time.deltaTime;
            Debug.Log(gameDuration);
            timerText.SetText(TimeFormatter.Formate(gameDuration));
            yield return null;
        }
    }
}
