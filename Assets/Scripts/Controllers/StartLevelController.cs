using System;
using UnityEngine;

public class StartLevelController : MonoBehaviour
{
    [SerializeField] private MazeSpawner _mazeSpawner;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private GreenScoresSpawner _greenScoresSpawner;
    [SerializeField] private PinkScoreSpawner _pinkScoreSpawner;
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private Timer _timer;
    [SerializeField] private InfoText _mazeInfoText;
    [SerializeField] private MazeAppearanceAnimation _mazeAppearanceAnimation;

    public static event Action OnAllSpawned;

    private void Start()
    {
        InitializeDefaultLevel();
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
        InitializeDefaultLevel();
        CircleAnimation.Instance.Decrease();
    }

    private void InitializeDefaultLevel()
    {
        _mazeSpawner.Spawn();
        _pinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _greenScoresSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _enemySpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _playerSpawner.Spawn(_mazeSpawner.FirstCellCoordinates);

        OnAllSpawned?.Invoke();
        _mazeAppearanceAnimation.Play(_mazeSpawner.CellObjects);
        SetMazeInfoText();
    }

    private void SetMazeInfoText()
    {
        _mazeInfoText.SetText(
            $"maze size: {_mazeSpawner.MazeWidth}x{_mazeSpawner.MazeHeight}  /" +
            $"  cycles: {_mazeSpawner.SpawnedCyclesCount}  /" +
            $"  eatable points: {_greenScoresSpawner.GreenScoresCount}"
            );
    }
}
