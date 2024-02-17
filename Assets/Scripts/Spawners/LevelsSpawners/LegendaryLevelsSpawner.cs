using Spawners.ObjectsSpawners;
using UnityEngine;

namespace Spawners.LevelsSpawners
{
    public class LegendaryLevelsSpawner : MonoBehaviour
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
                SpawnLuckyLevel
            };

            levelSpawnDelegates[Random.Range(0, levelSpawnDelegates.Length)]();
        }

        private void SpawnLuckyLevel()
        {
            mazeSpawner.Spawn(0, Random.Range(5, 9), Random.Range(5, 9));
            pinkScoreSpawner.Spawn(mazeSpawner.MazeWidth, mazeSpawner.MazeHeight);
            greenScoresSpawner.LuckySpawn(mazeSpawner.Maze, mazeSpawner.MazeWidth, mazeSpawner.MazeHeight);
            mazeAppearanceAnimation.Play(mazeSpawner.CellObjects);
            LevelDescription = "Congratulations, this is lucky temple, no enemies, no difficulties, just treasures!";
        }
    }
}
