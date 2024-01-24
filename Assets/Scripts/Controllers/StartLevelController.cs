using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StartLevelController : MonoBehaviour
{
    [SerializeField] private GameObject _educationalLevel;
    [SerializeField] private LevelSpawner _levelSpawner;
    [SerializeField] private InfoText _mazeInfoText;
    [SerializeField] private InfoText _levelDescriptionText;

    public static event Action OnAllSpawned;

    private void Awake()
    {
        _levelDescriptionText.GetComponent<Text>().DOFade(0f, 0f);
    }

    private void OnEnable()
    {
        FinishLevelController.OnReadyToStartNewLevel += InitializeLevel;
        FinishLevelController.OnLevelFinished += HideEducationalLevel;
    }

    private void OnDisable()
    {
        FinishLevelController.OnReadyToStartNewLevel -= InitializeLevel;
        FinishLevelController.OnLevelFinished -= HideEducationalLevel;
    }

    private void InitializeLevel()
    {
        _levelSpawner.SpawnLevel();
        SetMazeInfoText();
        SetLevelDescriptionText();
        _levelDescriptionText.GetComponent<Text>().DOFade(1f, 0.5f);
        StartCoroutine(StartingLevelCoroutine());
    }

    private IEnumerator StartingLevelCoroutine()
    {
        yield return new WaitForSeconds(2.5f);
        _levelDescriptionText.GetComponent<Text>().DOFade(0f, 0.15f);
        CircleAnimation.Instance.Decrease(0.2f);
        OnAllSpawned?.Invoke();
    }

    private void SetMazeInfoText()
    {
        _mazeInfoText.SetText(
            $"maze size: {_levelSpawner.MazeWidth}x{_levelSpawner.MazeHeight}  /" +
            $"  cycles: {_levelSpawner.MazeCyclesCount}  /" +
            $"  green tresaurs: {_levelSpawner.MazeGreenScoresCount}"
            );
    }

    private void SetLevelDescriptionText()
    {
        _levelDescriptionText.SetText(_levelSpawner.LevelDescription);
    }

    private void HideEducationalLevel()
    {
        _educationalLevel.SetActive(false);
    }
}
