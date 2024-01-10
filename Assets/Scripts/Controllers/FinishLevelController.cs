using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLevelController : MonoBehaviour
{
    [SerializeField] private InfoText _gameOverText;
    [SerializeField] private ScoreController _scoreController;
    [SerializeField] private Text _pressAnyKeyText;
    [SerializeField] private GameObject _circleAnimationPrefab;

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
        var circleAnimation = Instantiate(_circleAnimationPrefab).GetComponent<CircleAnimator>();
        circleAnimation.IncreaseCircle();

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
        var circleAnimation = Instantiate(_circleAnimationPrefab).GetComponent<CircleAnimator>();
        circleAnimation.IncreaseCircle();

        _gameOverText.SetText(_scoreController.GetScoresString());
        StartCoroutine(SpawnPressAnyKeyTextCoroutine());
    }

    private IEnumerator SpawnPressAnyKeyTextCoroutine()
    {
        yield return new WaitForSeconds(2f);
        _pressAnyKeyText.gameObject.SetActive(true);
    }
}
