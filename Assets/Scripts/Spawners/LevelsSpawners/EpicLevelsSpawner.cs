using Spawners.ObjectsSpawners;
using UnityEngine;

namespace Spawners.LevelsSpawners
{
    public class EpicLevelsSpawner : MonoBehaviour
    {
        [SerializeField] private MazeSpawner mazeSpawner;
        [SerializeField] private PinkScoreSpawner pinkScoreSpawner;
        [SerializeField] private GreenScoresSpawner greenScoresSpawner;

        [SerializeField] private MazeAppearanceAnimation mazeAppearanceAnimation;

        private delegate void LevelSpawnDelegate();

        public string LevelDescription { get; private set; }

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
            mazeSpawner.Spawn(Random.Range(3, 7));
            pinkScoreSpawner.Spawn(mazeSpawner.MazeWidth, mazeSpawner.MazeHeight);
            greenScoresSpawner.Spawn(mazeSpawner.Maze, mazeSpawner.MazeWidth, mazeSpawner.MazeHeight);
            mazeAppearanceAnimation.Play(mazeSpawner.CellObjects);
            LevelDescription = "Default temple, just default temple...";
        }
    }
}
