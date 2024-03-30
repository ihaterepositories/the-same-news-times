using UnityEngine;
using Zenject;

namespace Spawners.LevelsSpawners
{
    public class LegendaryLevelsSpawner
    {
        private readonly MazeSpawner _mazeSpawner;
        private readonly ObjectsSpawner _objectsSpawner;
        private readonly MazeAppearanceAnimation _mazeAppearanceAnimation;
        private delegate void LevelSpawnDelegate();

        public string LevelDescription { get; private set; }

        public LegendaryLevelsSpawner(MazeSpawner mazeSpawner, ObjectsSpawner objectsSpawner, MazeAppearanceAnimation mazeAppearanceAnimation)
        {
            _mazeSpawner = mazeSpawner;
            _objectsSpawner = objectsSpawner;
            _mazeAppearanceAnimation = mazeAppearanceAnimation;
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
            _mazeSpawner.Spawn(0, Random.Range(5, 9), Random.Range(5, 9));
            _objectsSpawner.PinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.GreenScoresSpawner.LuckySpawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _mazeAppearanceAnimation.Play(_mazeSpawner.CellObjects);
            LevelDescription = "Congratulations, this is lucky temple, no enemies, no difficulties, just treasures!";
        }
    }
}
