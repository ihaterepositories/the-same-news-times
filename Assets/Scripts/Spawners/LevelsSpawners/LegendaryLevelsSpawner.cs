using UnityEngine;

public class LegendaryLevelsSpawner : MonoBehaviour
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
            SpawnLuckyLevel
        };

        levelSpawnDelegates[Random.Range(0, levelSpawnDelegates.Length)]();
    }

    private void SpawnLuckyLevel()
    {
        _mazeSpawner.Spawn(0, Random.Range(5, 9), Random.Range(5, 9));
        _pinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _greenScoresSpawner.LuckySpawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        _mazeAppearanceAnimation.Play(_mazeSpawner.CellObjects);
        LevelDescription = "Congratulations, this is lucky temple, no enemies, no dificulties, just tresaures!";
    }
}
