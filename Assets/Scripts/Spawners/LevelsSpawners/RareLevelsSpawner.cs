using UnityEngine;

namespace Spawners.LevelsSpawners
{
    public class RareLevelsSpawner
    {
        private readonly MazeSpawner _mazeSpawner;
        private readonly ObjectsSpawner _objectsSpawner;
        private readonly MazeAppearanceAnimation _mazeAppearanceAnimation;
        private delegate void LevelSpawnDelegate();

        public string LevelDescription { get; private set; }
        
        public RareLevelsSpawner(MazeSpawner mazeSpawner, ObjectsSpawner objectsSpawner, MazeAppearanceAnimation mazeAppearanceAnimation)
        {
            _mazeSpawner = mazeSpawner;
            _objectsSpawner = objectsSpawner;
            _mazeAppearanceAnimation = mazeAppearanceAnimation;
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
            _mazeSpawner.Spawn(Random.Range(20, 25), Random.Range(30, 36), Random.Range(15, 19));
            _objectsSpawner.PinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            if (Random.Range(0, 5) == 0)
            {
                _objectsSpawner.LifeSaverSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            }
            _objectsSpawner.GreenScoresSpawner.GreenScoresCount = 0;
            _mazeAppearanceAnimation.Play(_mazeSpawner.CellObjects);
            LevelDescription = "Abandoned temple, there is nothing here...";
        }
    }
}
