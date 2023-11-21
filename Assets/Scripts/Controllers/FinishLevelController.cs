using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevelController : MonoBehaviour
{
    [SerializeField] private InfoText _gameOverText;
    [SerializeField] private ScoreController _scoreController;

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
        StartCoroutine(LoadGameSceneAsyncCoroutine());
    }

    private void FinishGame()
    {
        _gameOverText.SetText(_scoreController.GetScoresString());
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
