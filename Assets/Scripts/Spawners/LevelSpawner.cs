using Controllers.InGameControllers;
using Models;
using Spawners.ItemsSpawners;
using Spawners.LevelsSpawners;
using UnityEngine;

namespace Spawners
{
    public class LevelSpawner
    {
        private readonly DefaultLevelsSpawner _defaultLevelsSpawner;
        private readonly RareLevelsSpawner _rareLevelsSpawner;
        private readonly EpicLevelsSpawner _epicLevelsSpawner;
        private readonly LegendaryLevelsSpawner _legendaryLevelsSpawner;

        private readonly MazeSpawner _mazeSpawner;
        private readonly GreenScoresSpawner _greenScoresSpawner;

        private readonly Player _player;
        
        private int _lastLevelType;

        public int MazeWidth { get; private set; } 
        public int MazeHeight { get; private set; } 
        public int MazeCyclesCount { get; private set; }
        public int MazeGreenScoresCount { get; private set; }
        public string LevelDescription { get; private set; }
        public string RarityDescription { get; private set; }

        public LevelSpawner(
            DefaultLevelsSpawner defaultLevelsSpawner, 
            RareLevelsSpawner rareLevelsSpawner,
            EpicLevelsSpawner epicLevelsSpawner,
            LegendaryLevelsSpawner legendaryLevelsSpawner,
            MazeSpawner mazeSpawner,
            GreenScoresSpawner greenScoresSpawner,
            Player player)
        {
            _defaultLevelsSpawner = defaultLevelsSpawner;
            _rareLevelsSpawner = rareLevelsSpawner;
            _epicLevelsSpawner = epicLevelsSpawner;
            _legendaryLevelsSpawner = legendaryLevelsSpawner;
            _mazeSpawner = mazeSpawner;
            _greenScoresSpawner = greenScoresSpawner;
            _player = player;
        }
                
        public void Spawn()
        {
            var levelType = LevelRarityGenerator.GetLevelType();
            
            while (levelType == _lastLevelType)
            {
                levelType = LevelRarityGenerator.GetLevelType();
            }
            
            _lastLevelType = levelType;

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
                        Debug.Log("- level type generating error"); 
                        break;
            }

            MazeWidth = _mazeSpawner.MazeWidth - 1;
            MazeHeight = _mazeSpawner.MazeHeight - 1;
            MazeCyclesCount = _mazeSpawner.SpawnedCyclesCount;
            MazeGreenScoresCount = _greenScoresSpawner.GreenScoresCount;
            RarityDescription = LevelRarityGenerator.RarityDescription;
            _player.SetPosition(_mazeSpawner.FirstCellCoordinates);
            _player.ClearTrailRender();
        } 
    }
}
