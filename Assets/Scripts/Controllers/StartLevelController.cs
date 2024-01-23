using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StartLevelController : MonoBehaviour
{
    [SerializeField] private LevelSpawner _levelSpawner;
    [SerializeField] private InfoText _mazeInfoText;
    [SerializeField] private InfoText _levelDescriptionText;

    public static event Action OnAllSpawned;

    private void Awake()
    {
        _levelDescriptionText.GetComponent<Text>().DOFade(0f, 0f);
    }

    private void Start()
    {
        _levelSpawner.SpawnLevel();
        SetMazeInfoText();
        OnAllSpawned?.Invoke();
    }

    private void OnEnable()
    {
        FinishLevelController.OnReadyToStartNewLevel += InitializeLevel;
    }

    private void OnDisable()
    {
        FinishLevelController.OnReadyToStartNewLevel -= InitializeLevel;
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

    private void SetLevelDescriptionTextVisibile(float visibility)
    {
        _levelDescriptionText.GetComponent<Text>().DOFade(visibility, 1f);
    }

    private void SetLevelDescriptionText()
    {
        _levelDescriptionText.SetText(_levelSpawner.LevelDescription);
    }
}
