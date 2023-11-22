using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLevelController : MonoBehaviour
{
    [SerializeField] private InfoText _gameOverText;
    [SerializeField] private ScoreController _scoreController;
    [SerializeField] private Text _pressAnyKeyText;

    public static event Action OnFinishLevel;

    private void Awake()
    {
        _pressAnyKeyText.gameObject.SetActive(false);
    }

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

    private IEnumerator LoadGameSceneAsyncCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private void FinishGame()
    {
        _gameOverText.SetText(_scoreController.GetScoresString());
        StartCoroutine(SpawnPressAnyKeyTextCoroutine());
    }

    private IEnumerator SpawnPressAnyKeyTextCoroutine()
    {
        yield return new WaitForSeconds(2f);
        _pressAnyKeyText.gameObject.SetActive(true);
    }
}
