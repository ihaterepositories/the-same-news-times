using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FinishLevelController : MonoBehaviour
{
    [SerializeField] private InfoText _gameOverText;
    [SerializeField] private InGameScoreController _scoreController;
    [SerializeField] private Text _pressAnyKeyText;

    public static event Action OnLevelFinished;
    public static event Action OnGameFinished;

    private void Awake()
    {
        _pressAnyKeyText.gameObject.SetActive(false);
    }

    private void Update()
    {
        EnableGameStopping();
    }

    private void OnEnable()
    {
        InGameScoreController.OnPinkScoreUpdated += FinishLevel;
        Enemy.OnReachedPlayer += FinishGame;
    }

    private void OnDisable()
    {
        InGameScoreController.OnPinkScoreUpdated -= FinishLevel;
        Enemy.OnReachedPlayer -= FinishGame;
    }

    private void EnableGameStopping()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FinishGame();
        }
    }

    private void FinishLevel()
    {
        OnLevelFinished?.Invoke();
        SceneLoadingController.Instance.LoadSceneAsync("GameScene");
    }

    private void FinishGame()
    {
        OnGameFinished?.Invoke();
        CircleAnimation.Instance.Increase();
        _gameOverText.SetText(_scoreController.GetCurrentGameScore());
        StartCoroutine(ActivateExitButtonCoroutine());
    }

    private IEnumerator ActivateExitButtonCoroutine()
    {
        yield return new WaitForSeconds(2f);
        _pressAnyKeyText.gameObject.SetActive(true);
    }
}
