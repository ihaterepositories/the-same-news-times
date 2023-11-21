using System;
using System.Collections;
using UnityEngine;

public class FinishGameObject : MonoBehaviour
{
    public static event Action OnGameExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnGameExit?.Invoke();
        StartCoroutine(ExitGameCoroutine());
    }

    private IEnumerator ExitGameCoroutine()
    {
        yield return new WaitForSeconds(1f);
        QuitGame();
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
