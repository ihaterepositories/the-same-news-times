using System.Collections;
using UnityEngine;

public class RareLevelsSpawner : MonoBehaviour
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
            SpawnAbandonedLevel
        };

        levelSpawnDelegates[Random.Range(0, levelSpawnDelegates.Length)]();
    }

    private void SpawnAbandonedLevel()
    {
        _mazeSpawner.Spawn(Random.Range(20, 25), Random.Range(30, 36), Random.Range(15, 19));
        _pinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _greenScoresSpawner.GreenScoresCount = 0;
        _mazeAppearanceAnimation.Play(_mazeSpawner.CellObjects);
        LevelDescription = "Abandoned temple, there is nothing here...";
    }
}
