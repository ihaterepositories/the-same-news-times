using Animations;
using Constants;
using UnityEngine;

namespace Spawners.LevelsSpawners
{
    public class RareLevelsSpawner
    {
        private readonly MazeSpawner _mazeSpawner;
        private readonly ObjectsSpawner _objectsSpawner;
        
        private delegate void LevelSpawnDelegate();

        public string LevelDescription { get; private set; }
        
        public RareLevelsSpawner(
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
                SpawnAbandonedLevel
            };

            levelSpawnDelegates[Random.Range(0, levelSpawnDelegates.Length)]();
        }

        private void SpawnAbandonedLevel()
        {
            _mazeSpawner.Spawn(
        Random.Range(20, 25), 
         Random.Range(MazeSizes.MinWidthAbandonedLevel, 36), 
         Random.Range(MazeSizes.MinHeightAbandonedLevel, 19));
            
            _objectsSpawner.PinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            if (Random.Range(0, 3) == 0)
            {
                _objectsSpawner.LifeSaverSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            }
            if (Random.Range(0, 2) == 1)
            {
                _objectsSpawner.PoisonSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            }
            _objectsSpawner.GreenScoresSpawner.GreenScoresCount = 0;
            LevelDescription = LevelDescriptions.AbandonedLevel;
        }
    }
}
