using Controllers;
using Spawners.LevelsSpawners;
using Spawners.ObjectsSpawners;
using UnityEngine;

namespace Spawners
{
        public class LevelSpawner : MonoBehaviour
        {
                [SerializeField] private DefaultLevelsSpawner defaultLevelsSpawner;
                [SerializeField] private RareLevelsSpawner rareLevelsSpawner;
                [SerializeField] private EpicLevelsSpawner epicLevelsSpawner;
                [SerializeField] private LegendaryLevelsSpawner legendaryLevelsSpawner;

                [SerializeField] private MazeSpawner mazeSpawner;
                [SerializeField] private GreenScoresSpawner greenScoresSpawner;
                [SerializeField] private PlayerSpawner playerSpawner;

                public int MazeWidth { get; private set; } 
                public int MazeHeight { get; private set; } 
                public int MazeCyclesCount { get; private set; }
                public int MazeGreenScoresCount { get; private set; }
                public string LevelDescription { get; private set; }
                public string RarityDescription { get; private set; }
    
                public void Spawn()
                {
                        int levelType = LevelRarityController.GetLevelType();

                        switch (levelType)
                        {
                                case 1: legendaryLevelsSpawner.SpawnRandomLevel();
                                        LevelDescription = legendaryLevelsSpawner.LevelDescription;
                                        break;
                                case 2: epicLevelsSpawner.SpawnRandomLevel();
                                        LevelDescription = epicLevelsSpawner.LevelDescription;
                                        break;
                                case 3: rareLevelsSpawner.SpawnRandomLevel();
                                        LevelDescription = rareLevelsSpawner.LevelDescription;
                                        break;
                                case 4: defaultLevelsSpawner.SpawnRandomLevel();
                                        LevelDescription = defaultLevelsSpawner.LevelDescription;
                                        break;

                                default:
                                        Debug.Log("- level type generating error"); 
                                        break;
                        }

                        MazeWidth = mazeSpawner.MazeWidth - 1;
                        MazeHeight = mazeSpawner.MazeHeight - 1;
                        MazeCyclesCount = mazeSpawner.SpawnedCyclesCount;
                        MazeGreenScoresCount = greenScoresSpawner.GreenScoresCount;
                        RarityDescription = LevelRarityController.RarityDescription;
                        playerSpawner.Spawn(mazeSpawner.FirstCellCoordinates);
                }
        }
}
