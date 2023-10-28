using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelController : MonoBehaviour
{
    [SerializeField] private MazeSpawner _mazeSpawner;
    [SerializeField] private GameObject _playerPrefab;

    private void Start()
    {
        _mazeSpawner.Spawn();
        Instantiate(_playerPrefab, new Vector2(_mazeSpawner.FirstCellPositionX, _mazeSpawner.FirstCellPositionY), Quaternion.identity);
    }
}
