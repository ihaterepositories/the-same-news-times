using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameObject : MonoBehaviour
{
    public static event Action OnGameStart;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPrefs.SetFloat("TimerSeconds", 300f);
        PlayerPrefs.SetInt("GreenScore", 0);
        PlayerPrefs.SetInt("PinkScore", 0);

        OnGameStart?.Invoke();
        StartCoroutine(LoadGameSceneCoroutine());
    }

    private IEnumerator LoadGameSceneCoroutine()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(LoadGameSceneAsyncCoroutine());
    }

    private IEnumerator LoadGameSceneAsyncCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
