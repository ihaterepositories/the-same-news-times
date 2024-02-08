using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpicLevelsSpawner : MonoBehaviour
{
    [SerializeField] private MazeSpawner _mazeSpawner;
    [SerializeField] private PinkScoreSpawner _pinkScoreSpawner;
    [SerializeField] private GreenScoresSpawner _greenScoresSpawner;

    [SerializeField] private MazeAppearanceAnimation _mazeAppearanceAnimation;

    private delegate void LevelSpawnDelegate();

    public string LevelDescription { get; private set; }

    public void SpawnRandomLevel()
    {
        LevelSpawnDelegate[] levelSpawnDelegates =
        {
            SpawnDefaultLevel
        };

        levelSpawnDelegates[Random.Range(0, levelSpawnDelegates.Length)]();
    }

    private void SpawnDefaultLevel()
    {
        _mazeSpawner.Spawn(Random.Range(3, 7));
        _pinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _greenScoresSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _mazeAppearanceAnimation.Play(_mazeSpawner.CellObjects);
        LevelDescription = "Default temple, just default temple...";
    }
}
