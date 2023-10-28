using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelController : MonoBehaviour
{
    [SerializeField] private MazeSpawner _mazeSpawner;

    private void Start()
    {
        _mazeSpawner.Spawn();
    }
}
