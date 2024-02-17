using Spawners.ObjectsSpawners;
using UnityEngine;

namespace Spawners.LevelsSpawners
{
    public class RareLevelsSpawner : MonoBehaviour
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
                SpawnAbandonedLevel
            };

            levelSpawnDelegates[Random.Range(0, levelSpawnDelegates.Length)]();
        }

        private void SpawnAbandonedLevel()
        {
            mazeSpawner.Spawn(Random.Range(20, 25), Random.Range(30, 36), Random.Range(15, 19));
            pinkScoreSpawner.Spawn(mazeSpawner.MazeWidth, mazeSpawner.MazeHeight);
            greenScoresSpawner.GreenScoresCount = 0;
            mazeAppearanceAnimation.Play(mazeSpawner.CellObjects);
            LevelDescription = "Abandoned temple, there is nothing here...";
        }
    }
}
