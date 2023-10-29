using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLevelController : MonoBehaviour
{
    [SerializeField] private MazeSpawner _mazeSpawner;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _exitObject;

    public static event Action<int, int> OnMazeGenerated;

    private void Start()
    {
        _mazeSpawner.Spawn();
        OnMazeGenerated?.Invoke(_mazeSpawner.MazeSize, _mazeSpawner.SpawnedCyclesCount);
        SpawnPlayer();
        SpawnExitObject();
    }

    private void SpawnPlayer()
    {
        Instantiate(_playerPrefab, _mazeSpawner.FirstCellCoordinates, Quaternion.identity);
    }

    private void SpawnExitObject()
    {
        Instantiate(
            _exitObject, 
            new Vector2(MazeGenerator.ExitCellPositionX - (_mazeSpawner.MazeSize / 2) + 0.9f, MazeGenerator.ExitCellPositionY - (_mazeSpawner.MazeSize / 2) + 0.9f), 
            Quaternion.identity);
    }
}
