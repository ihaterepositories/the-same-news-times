using System;
using UnityEngine;

public class StartLevelController : MonoBehaviour
{
    [SerializeField] private MazeSpawner _mazeSpawner;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private InfoText _mazeInfoText;
    [SerializeField] private GameObject _circleAnimationPrefab;

    public static event Action OnAllSpawned;

    private void Start()
    {
        var circleAnimation = Instantiate(_circleAnimationPrefab).GetComponent<CircleAnimator>();
        circleAnimation.DecreaseCircle();

        _mazeSpawner.Spawn();

        _mazeInfoText.SetText(
            $"maze size: {_mazeSpawner.MazeWidth}x{_mazeSpawner.MazeHeight}  /" +
            $"  cycles: {_mazeSpawner.SpawnedCyclesCount}  /" +
            $"  eatable points: {_mazeSpawner.SpawnedEatablePointsCount}"
            );

        SpawnPlayer();

        OnAllSpawned?.Invoke();
    }

    private void SpawnPlayer()
    {
        Instantiate(_playerPrefab, _mazeSpawner.FirstCellCoordinates, Quaternion.identity);
    }
}
