using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevelController : MonoBehaviour
{
    public static event Action OnFinishLevel;

    private void OnEnable()
    {
        ScoreController.OnPinkScoreUpdated += FinishLevel;
        Timer.OnTimerFinish += FinishGame;
    }

    private void OnDisable()
    {
        ScoreController.OnPinkScoreUpdated -= FinishLevel;
        Timer.OnTimerFinish -= FinishGame;
    }

    private void FinishLevel()
    {
        StartCoroutine(FinishLevelCoroutine());
    }

    private IEnumerator FinishLevelCoroutine()
    {
        OnFinishLevel?.Invoke();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameScene");
    }

    private void FinishGame()
    {
        //show finish game window
    }
}
