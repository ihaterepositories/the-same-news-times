using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameObject : MonoBehaviour
{
    public static event Action OnGameStart;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPrefs.SetInt("GreenScore", 0);
        PlayerPrefs.SetInt("PinkScore", 0);

        OnGameStart?.Invoke();
        StartCoroutine(LoadTimerSettingSceneCoroutine());
    }

    private IEnumerator LoadTimerSettingSceneCoroutine()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(LoadTimerSettingSceneCoroutineAsync());
    }

    private IEnumerator LoadTimerSettingSceneCoroutineAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TimerSettingScene");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
