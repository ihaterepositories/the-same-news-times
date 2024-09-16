using Animations;
using Constants;
using DataTransfers;
using Spawners.Enumerations;
using UnityEngine;
using Zenject;

namespace Spawners
{
    public class LevelConstructor
    {
        private MazeSpawner _mazeSpawner;
        private ObjectsSpawner _objectsSpawner;
        private MazeDisappearanceAnimation _mazeDisappearanceAnimation;
        private delegate void LevelSpawnDelegate();
        private MazeData mazeData;
        
        public string LevelDescription { get; private set; }
        public LevelName LevelName { get; private set; }
        
        [Inject]
        private void Construct(
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
                SpawnTrappedLevel,
                SpawnInvisibleLevel
                // SpawnDefaultLevel,
                // SpawnLuckyLevel,
                // SpawnAbandonedLevel
            };

            levelSpawnDelegates[Random.Range(0, levelSpawnDelegates.Length)]();
        }
        
        private void SpawnTempleKeeperLevel()
        {
            _mazeSpawner.Spawn(Random.Range(1, 5));
            FillMazeData();
            
            _objectsSpawner.MazeExitSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.TempleKeeperSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.ModificatorsSpawner.Spawn(mazeData, 2);
            _objectsSpawner.PointsSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            
            LevelDescription = LevelDescriptions.TempleKeeperLevel;
            LevelName = LevelName.TempleKeeper;
        }

        private void SpawnTrappedLevel()
        {
            _mazeSpawner.Spawn(Random.Range(3, 6));
            FillMazeData();
            
            _objectsSpawner.MazeExitSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.TrapSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.ModificatorsSpawner.Spawn(mazeData, 2);
            _objectsSpawner.PointsSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight, 2);

            LevelDescription = LevelDescriptions.LockedLevel;
            LevelName = LevelName.Trapped;
        }

        private void SpawnInvisibleLevel()
        {
            _mazeSpawner.Spawn(
                0, 
                Random.Range(MazeSizes.MinWidthInvisibleLevel, MazeSizes.MaxWidthInvisibleLevel), 
                Random.Range(MazeSizes.MinHeightInvisibleLevel, MazeSizes.MaxHeightInvisibleLevel)
                );
            FillMazeData();
            
            _objectsSpawner.MazeExitSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.GhostSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.PointsSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            
            _mazeDisappearanceAnimation.PlayForInvisibleLevel(_mazeSpawner.CellObjects);
            
            LevelDescription = LevelDescriptions.InvisibleLevel;
            LevelName = LevelName.Ghostly;
        }

        private void FillMazeData()
        {
            mazeData = new MazeData(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
        }
        
        private void SpawnDefaultLevel()
        {
            _mazeSpawner.Spawn(Random.Range(3, 7));
            _objectsSpawner.MazeExitSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.PointsSpawner.Spawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            
            LevelDescription = LevelDescriptions.DefaultLevel;
        }
        
        private void SpawnLuckyLevel()
        {
            _mazeSpawner.Spawn(
                0, 
                Random.Range(MazeSizes.MinWidthLuckyLevel, MazeSizes.MaxWidthLuckyLevel), 
                Random.Range(MazeSizes.MinHeightLuckyLevel, MazeSizes.MaxHeightLuckyLevel));
            
            _objectsSpawner.MazeExitSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            _objectsSpawner.PointsSpawner.LuckySpawn(_mazeSpawner.Maze, _mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            LevelDescription = LevelDescriptions.LuckyLevel;
        }
        
        private void SpawnAbandonedLevel()
        {
            _mazeSpawner.Spawn(
                Random.Range(20, 25), 
                Random.Range(MazeSizes.MinWidthAbandonedLevel, MazeSizes.MaxWidthAbandonedLevel), 
                Random.Range(MazeSizes.MinHeightAbandonedLevel, MazeSizes.MaxHeightAbandonedLevel));
            
            _objectsSpawner.MazeExitSpawner.Spawn(_mazeSpawner.MazeWidth, _mazeSpawner.MazeHeight);
            
            _objectsSpawner.PointsSpawner.GreenScoresCount = 0;
            LevelDescription = LevelDescriptions.AbandonedLevel;
        }
    }
}