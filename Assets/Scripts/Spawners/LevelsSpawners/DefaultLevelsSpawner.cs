using Animations;
using Constants;
using UnityEngine;

namespace Spawners.LevelsSpawners
{
    public class DefaultLevelsSpawner
    {
        private readonly MazeSpawner _mazeSpawner;
        private readonly ObjectsSpawner _objectsSpawner;
        private readonly MazeDisappearanceAnimation _mazeDisappearanceAnimation;
        
        private delegate void LevelSpawnDelegate();

        public string LevelDescription { get; private set; }
        
        public DefaultLevelsSpawner(
            MazeSpawner mazeSpawner, 
            ObjectsSpawner objectsSpawner,
            MazeDisappearanceAnimation mazeDisappearanceAnimation)
        {
            _mazeSpawner = mazeSpawner;
            _objectsSpawner = objectsSpawner;
            _mazeDisappearanceAnimation = mazeDisappearanceAnimation;
        }
        
        public void SpawnRandomLevel()
        {
            LevelSpawnDelegate[] levelSpawnDelegates =
            {
                SpawnTempleKeeperLevel,
                SpawnLockedLevel,
                SpawnInvisibleLevel
            };

            levelSpawnDelegates[Random.Range(0, levelSpawnDelegates.Length)]();
        }

        private void SpawnTempleKeeperLevel()
        {
            _mazeSpawner.Spawn(Random.Range(1, 5));
            _objectsSpawner.PinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.TempleKeeperSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.GreenScoresSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            LevelDescription = LevelDescriptions.TempleKeeperLevel;
        }

        private void SpawnLockedLevel()
        {
            _mazeSpawner.Spawn(Random.Range(10, 15));
            _objectsSpawner.PinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.LockSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.KeySpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.TrapSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.GreenScoresSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight, 2);
            if (Random.Range(0, 2) == 1)
            {
                _objectsSpawner.PoisonSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            }

            LevelDescription = LevelDescriptions.LockedLevel;
        }

        private void SpawnInvisibleLevel()
        {
            _mazeSpawner.Spawn(
                0, 
                Random.Range(MazeSizes.MinWidthInvisibleLevel, MazeSizes.MaxWidthInvisibleLevel), 
                Random.Range(MazeSizes.MinHeightInvisibleLevel, MazeSizes.MaxHeightInvisibleLevel));
            
            _objectsSpawner.PinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.GhostSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.GreenScoresSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            if (Random.Range(0, 2) == 1)
            {
                _objectsSpawner.PoisonSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            }
            _mazeDisappearanceAnimation.PlayForInvisibleLevel(_mazeSpawner.CellObjects);
            LevelDescription = LevelDescriptions.InvisibleLevel;
        }
    }
}
