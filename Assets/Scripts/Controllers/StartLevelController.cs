using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLevelController : MonoBehaviour
{
    [SerializeField] private MazeSpawner _mazeSpawner;
    [SerializeField] private GameObject _playerPrefab;

    public static event Action OnStartLevel;
    public static event Action<int, int, int> OnMazeGenerated;
    public static event Action OnAllSpawned;

    private void Start()
    {
        OnStartLevel?.Invoke();
        _mazeSpawner.Spawn();
        OnMazeGenerated?.Invoke(_mazeSpawner.MazeSize, _mazeSpawner.SpawnedCyclesCount, _mazeSpawner.SpawnedEatablePointsCount);
        SpawnPlayer();
        OnAllSpawned?.Invoke();
    }

    private void SpawnPlayer()
    {
        Instantiate(_playerPrefab, _mazeSpawner.FirstCellCoordinates, Quaternion.identity);
    }
}
