using Animations;
using Constants;
using UnityEngine;

namespace Spawners.LevelsSpawners
{
    public class EpicLevelsSpawner
    {
        private readonly MazeSpawner _mazeSpawner;
        private readonly ObjectsSpawner _objectsSpawner;
        
        private delegate void LevelSpawnDelegate();

        public string LevelDescription { get; private set; }
        
        public EpicLevelsSpawner(
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
                SpawnDefaultLevel
            };

            levelSpawnDelegates[Random.Range(0, levelSpawnDelegates.Length)]();
        }

        private void SpawnDefaultLevel()
        {
            _mazeSpawner.Spawn(Random.Range(3, 7));
            _objectsSpawner.PinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.GreenScoresSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            if (Random.Range(0, 3) == 0)
            {
                _objectsSpawner.BoosterSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            }
            if (Random.Range(0, 2) == 1)
            {
                _objectsSpawner.PoisonSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            }
            LevelDescription = LevelDescriptions.DefaultLevel;
        }
    }
}
