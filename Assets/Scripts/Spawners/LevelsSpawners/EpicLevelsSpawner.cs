using AnimationControllers;
using UnityEngine;

namespace Spawners.LevelsSpawners
{
    public class EpicLevelsSpawner
    {
        private readonly MazeSpawner _mazeSpawner;
        private readonly ObjectsSpawner _objectsSpawner;
        private readonly MazeAppearanceAnimation _mazeAppearanceAnimation;
        private delegate void LevelSpawnDelegate();

        public string LevelDescription { get; private set; }
        
        public EpicLevelsSpawner(MazeSpawner mazeSpawner, ObjectsSpawner objectsSpawner, MazeAppearanceAnimation mazeAppearanceAnimation)
        {
            _mazeSpawner = mazeSpawner;
            _objectsSpawner = objectsSpawner;
            _mazeAppearanceAnimation = mazeAppearanceAnimation;
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
            if (Random.Range(0, 1) == 0)
            {
                _objectsSpawner.BoosterSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            }
            _mazeAppearanceAnimation.Play(_mazeSpawner.CellObjects);
            LevelDescription = "Default temple, just default temple...";
        }
    }
}
