using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLevelController : MonoBehaviour
{
    [SerializeField] private InfoText _gameOverText;
    [SerializeField] private ScoreController _scoreController;
    [SerializeField] private Text _pressAnyKeyText;

    private void Awake()
    {
        _pressAnyKeyText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        ScoreController.OnPinkScoreUpdated += FinishLevel;
        Timer.OnTimerFinish += FinishGame;
        Enemy.OnReachedPlayer += FinishGame;
    }

    private void OnDisable()
    {
        ScoreController.OnPinkScoreUpdated -= FinishLevel;
        Timer.OnTimerFinish -= FinishGame;
        Enemy.OnReachedPlayer -= FinishGame;
    }

    private void FinishLevel()
    {
        StartCoroutine(FinishLevelCoroutine());
    }

    private IEnumerator FinishLevelCoroutine()
    {
        CircleAnimation.Instance.Increase(4);

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
        CircleAnimation.Instance.Increase(4);

        _gameOverText.SetText(_scoreController.GetScoresString());
        StartCoroutine(ActivatePressAnyKeyTextCoroutine());
    }

    private IEnumerator ActivatePressAnyKeyTextCoroutine()
    {
        yield return new WaitForSeconds(2f);
        _pressAnyKeyText.gameObject.SetActive(true);
    }
}
