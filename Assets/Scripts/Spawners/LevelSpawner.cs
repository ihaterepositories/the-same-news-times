using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private DefaultLevelsSpawner _defaultLevelsSpawner;
    [SerializeField] private RareLevelsSpawner _rareLevelsSpawner;
    [SerializeField] private EpicLevelsSpawner _epicLevelsSpawner;
    [SerializeField] private LegendaryLevelsSpawner _legendaryLevelsSpawner;

    [SerializeField] private MazeSpawner _mazeSpawner;
    [SerializeField] private GreenScoresSpawner _greenScoresSpawner;
    [SerializeField] private PlayerSpawner _playerSpawner;

    public int MazeWidth { get; private set; } 
    public int MazeHeight { get; private set; } 
    public int MazeCyclesCount { get; private set; }
    public int MazeGreenScoresCount { get; private set; }
    public string LevelDescription { get; private set; }
    public string RarityDescription { get; private set; }

    public void SpawnLevel()
    {
        int levelType = LevelRarityController.GetLevelType();

        switch (levelType)
        {
            case 1: _legendaryLevelsSpawner.SpawnRandomLevel();
                    LevelDescription = _legendaryLevelsSpawner.LevelDescription;
                    break;
            case 2: _epicLevelsSpawner.SpawnRandomLevel();
                    LevelDescription = _epicLevelsSpawner.LevelDescription;
                    break;
            case 3: _rareLevelsSpawner.SpawnRandomLevel();
                    LevelDescription = _rareLevelsSpawner.LevelDescription;
                    break;
            case 4: _defaultLevelsSpawner.SpawnRandomLevel();
                    LevelDescription = _defaultLevelsSpawner.LevelDescription;
                    break;

            default:
                SpawnLevel();
                Debug.Log("- level type error, trying to regenerate..."); 
                break;
        }

        MazeWidth = _mazeSpawner.MazeWidth - 1;
        MazeHeight = _mazeSpawner.MazeHeight - 1;
        MazeCyclesCount = _mazeSpawner.SpawnedCyclesCount;
        MazeGreenScoresCount = _greenScoresSpawner.GreenScoresCount;
        RarityDescription = LevelRarityController.RarityDescription;

        if (FindObjectOfType<Player>() == null)
        _playerSpawner.Spawn(_mazeSpawner.FirstCellCoordinates);
    }
}
