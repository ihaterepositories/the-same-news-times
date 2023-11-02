using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevelController : MonoBehaviour
{
    public static event Action OnFinishLevel;

    private void OnEnable()
    {
        ExitObject.OnEated += FinishLevel;
    }

    private void OnDisable()
    {
        ExitObject.OnEated -= FinishLevel;
    }

    private void FinishLevel()
    {
        StartCoroutine(FinishLevelCoroutine());
    }

    private IEnumerator FinishLevelCoroutine()
    {
        OnFinishLevel?.Invoke();
        yield return new WaitForSeconds(1f);
        LoadNewLevel();
    }

    private void LoadNewLevel()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
