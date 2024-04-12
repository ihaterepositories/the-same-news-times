using Animations;
using Constants;
using UnityEngine;

namespace Spawners.LevelsSpawners
{
    public class LegendaryLevelsSpawner
    {
        private readonly MazeSpawner _mazeSpawner;
        private readonly ObjectsSpawner _objectsSpawner;
        
        private delegate void LevelSpawnDelegate();

        public string LevelDescription { get; private set; }

        public LegendaryLevelsSpawner(
            MazeSpawner mazeSpawner, 
            ObjectsSpawner objectsSpawner)
        {
            _mazeSpawner = mazeSpawner;
            _objectsSpawner = objectsSpawner;
        }
        
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
            _mazeSpawner.Spawn(
                0, 
                Random.Range(MazeSizes.MinWidthLuckyLevel, MazeSizes.MaxWidthLuckyLevel), 
                Random.Range(MazeSizes.MinHeightLuckyLevel, MazeSizes.MaxHeightLuckyLevel));
            
            _objectsSpawner.PinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.GreenScoresSpawner.LuckySpawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            LevelDescription = LevelDescriptions.LuckyLevel;
        }
    }
}
