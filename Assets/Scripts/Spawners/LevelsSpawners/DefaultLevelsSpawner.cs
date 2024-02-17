using Spawners.ObjectsSpawners;
using UnityEngine;

namespace Spawners.LevelsSpawners
{
    public class DefaultLevelsSpawner : MonoBehaviour
    {
        [SerializeField] private MazeSpawner mazeSpawner;
        [SerializeField] private PinkScoreSpawner pinkScoreSpawner;
        [SerializeField] private GreenScoresSpawner greenScoresSpawner;
        [SerializeField] private TempleKeeperSpawner templeKeeperSpawner;
        [SerializeField] private LockSpawner lockSpawner;
        [SerializeField] private KeySpawner keySpawner;
        [SerializeField] private TrapSpawner trapSpawner;
        [SerializeField] private GhostSpawner ghostSpawner;

        [SerializeField] private MazeAppearanceAnimation mazeAppearanceAnimation;

        private delegate void LevelSpawnDelegate();

        public string LevelDescription { get; private set; }

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
            mazeSpawner.Spawn(Random.Range(1, 5));
            pinkScoreSpawner.Spawn(mazeSpawner.MazeWidth, mazeSpawner.MazeHeight);
            templeKeeperSpawner.Spawn(mazeSpawner.Maze, mazeSpawner.MazeWidth, mazeSpawner.MazeHeight);
            greenScoresSpawner.Spawn(mazeSpawner.Maze, mazeSpawner.MazeWidth, mazeSpawner.MazeHeight);
            mazeAppearanceAnimation.Play(mazeSpawner.CellObjects);
            LevelDescription = "Be careful, this temple is guarded by the temple keeper!";
        }

        private void SpawnLockedLevel()
        {
            mazeSpawner.Spawn(Random.Range(10, 15));
            pinkScoreSpawner.Spawn(mazeSpawner.MazeWidth, mazeSpawner.MazeHeight);
            lockSpawner.Spawn(mazeSpawner.MazeWidth, mazeSpawner.MazeHeight);
            keySpawner.Spawn(mazeSpawner.Maze, mazeSpawner.MazeWidth, mazeSpawner.MazeHeight);
            trapSpawner.Spawn(mazeSpawner.Maze, mazeSpawner.MazeWidth, mazeSpawner.MazeHeight);
            greenScoresSpawner.Spawn(mazeSpawner.Maze, mazeSpawner.MazeWidth, mazeSpawner.MazeHeight, 2);
            mazeAppearanceAnimation.Play(mazeSpawner.CellObjects);
            LevelDescription = "The exit of this temple is locked, find the key and be aware from traps!";
        }

        private void SpawnInvisibleLevel()
        {
            mazeSpawner.Spawn(0, Random.Range(6, 10), Random.Range(6, 10));
            pinkScoreSpawner.Spawn(mazeSpawner.MazeWidth, mazeSpawner.MazeHeight);
            ghostSpawner.Spawn(mazeSpawner.Maze, mazeSpawner.MazeWidth, mazeSpawner.MazeHeight);
            greenScoresSpawner.Spawn(mazeSpawner.Maze, mazeSpawner.MazeWidth, mazeSpawner.MazeHeight);
            mazeAppearanceAnimation.PlayForInvisibleLevel(mazeSpawner.CellObjects);
            LevelDescription = "This temple have invisible walls, be aware of ghosts from other dimension!";
        }
    }
}
