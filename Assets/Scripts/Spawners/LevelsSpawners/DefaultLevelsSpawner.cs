using AnimationControllers;
using UnityEngine;

namespace Spawners.LevelsSpawners
{
    public class DefaultLevelsSpawner
    {
        private readonly MazeSpawner _mazeSpawner;
        private readonly ObjectsSpawner _objectsSpawner;
        private readonly MazeAppearanceAnimation _mazeAppearanceAnimation;
        private delegate void LevelSpawnDelegate();

        public string LevelDescription { get; private set; }
        
        public DefaultLevelsSpawner(MazeSpawner mazeSpawner, ObjectsSpawner objectsSpawner, MazeAppearanceAnimation mazeAppearanceAnimation)
        {
            _mazeSpawner = mazeSpawner;
            _objectsSpawner = objectsSpawner;
            _mazeAppearanceAnimation = mazeAppearanceAnimation;
        }
        
        public void SpawnRandomLevel()
        {
            LevelSpawnDelegate[] levelSpawnDelegates =
            {
                SpawnEnemyLevel,
                SpawnLockedLevel,
                SpawnInvisibleLevel
            };

            levelSpawnDelegates[Random.Range(0, levelSpawnDelegates.Length)]();
        }

        private void SpawnEnemyLevel()
        {
            _mazeSpawner.Spawn(Random.Range(1, 5));
            _objectsSpawner.PinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.TempleKeeperSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.GreenScoresSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _mazeAppearanceAnimation.Play(_mazeSpawner.CellObjects);
            LevelDescription = "Be careful, this temple is guarded by the temple keeper!";
        }

        private void SpawnLockedLevel()
        {
            _mazeSpawner.Spawn(Random.Range(10, 15));
            _objectsSpawner.PinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.LockSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.KeySpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.TrapSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.GreenScoresSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight, 2);
            _mazeAppearanceAnimation.Play(_mazeSpawner.CellObjects);
            LevelDescription = "The exit of this temple is locked, find the key and be aware from traps!";
        }

        private void SpawnInvisibleLevel()
        {
            _mazeSpawner.Spawn(0, Random.Range(6, 10), Random.Range(6, 10));
            _objectsSpawner.PinkScoreSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.GhostSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.GreenScoresSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _mazeAppearanceAnimation.PlayForInvisibleLevel(_mazeSpawner.CellObjects);
            LevelDescription = "This temple have invisible walls, be aware of ghosts from other dimension!";
        }
    }
}
