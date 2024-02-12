using System.Collections;
using System.Collections.Generic;
using Spawners.ObjectsSpawners;
using UnityEngine;

public class DefaultLevelsSpawner : MonoBehaviour
{
    [SerializeField] private MazeSpawner _mazeSpawner;
    [SerializeField] private PinkScoreSpawner _pinkScoreSpawner;
    [SerializeField] private GreenScoresSpawner _greenScoresSpawner;
    [SerializeField] private TempleKeeperSpawner _templeKeeperSpawner;
    [SerializeField] private LockSpawner _lockSpawner;
    [SerializeField] private KeySpawner _keySpawner;
    [SerializeField] private TrapSpawner _trapSpawner;
    [SerializeField] private GhostSpawner _ghostSpawner;

    [SerializeField] private MazeAppearanceAnimation _mazeAppearanceAnimation;

    private delegate void LevelSpawnDelegate();

    public string LevelDescription { get; private set; }

    public void SpawnRandomLevel()
    {
        LevelSpawnDelegate[] levelSpawnDelegates =
        {
            //SpawnEnemyLevel,
            //SpawnLockedLevel,
            SpawnUnvisibleLevel
        };

        levelSpawnDelegates[Random.Range(0, levelSpawnDelegates.Length)]();
    }

    private void SpawnEnemyLevel()
    {
        _mazeSpawner.Spawn(Random.Range(1, 5));
        _pinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _templeKeeperSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _greenScoresSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _mazeAppearanceAnimation.Play(_mazeSpawner.CellObjects);
        LevelDescription = "Be careful, this temple is guarded by the temple keeper!";
    }

    private void SpawnLockedLevel()
    {
        _mazeSpawner.Spawn(Random.Range(10, 15));
        _pinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _lockSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _keySpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _trapSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _greenScoresSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight, 2);
        _mazeAppearanceAnimation.Play(_mazeSpawner.CellObjects);
        LevelDescription = "The exit of this temple is locked, find the key and be aware from traps!";
    }

    private void SpawnUnvisibleLevel()
    {
        _mazeSpawner.Spawn(0, Random.Range(6, 10), Random.Range(6, 10));
        _pinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _ghostSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _greenScoresSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _mazeAppearanceAnimation.PlayForInvisibleLevel(_mazeSpawner.CellObjects);
        LevelDescription = "This temple have uvisible walls, be aware of ghosts from other dimension!";
    }
}
