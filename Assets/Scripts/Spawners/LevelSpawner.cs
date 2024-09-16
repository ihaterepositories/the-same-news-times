using Controllers.InGameControllers;
using Models;
using Spawners.Enumerations;
using Spawners.ItemsSpawners;
using UnityEngine;
using Zenject;

namespace Spawners
{
    public class LevelSpawner
    {
        private LevelConstructor _levelConstructor;
        private MazeSpawner _mazeSpawner;
        private PointsSpawner _pointsSpawner;
        private Player _player;

        public int MazeWidth { get; private set; } 
        public int MazeHeight { get; private set; } 
        public int MazeCyclesCount { get; private set; }
        public int MazeGreenScoresCount { get; private set; }
        public string LevelDescription { get; private set; }
        public string RarityDescription { get; private set; }
        public float LevelDuration { get; private set; }

        [Inject]
        private void Construct(
            LevelConstructor levelConstructor,
            MazeSpawner mazeSpawner,
            PointsSpawner pointsSpawner,
            Player player)
        {
            _levelConstructor = levelConstructor;
            _mazeSpawner = mazeSpawner;
            _pointsSpawner = pointsSpawner;
            _player = player;
        }
                
        public void Spawn()
        {
            _levelConstructor.SpawnRandomLevel();

            LevelDuration = CalculateLevelDuration();
            LevelDescription = _levelConstructor.LevelDescription;
            MazeWidth = _mazeSpawner.MazeWidth - 1;
            MazeHeight = _mazeSpawner.MazeHeight - 1;
            MazeCyclesCount = _mazeSpawner.SpawnedCyclesCount;
            MazeGreenScoresCount = _pointsSpawner.GreenScoresCount;
            RarityDescription = LevelRarityGenerator.RarityDescription;
            _player.SetPosition(_mazeSpawner.FirstCellCoordinates);
            _player.ClearTrailRender();
        } 
        
        private float CalculateLevelDuration()
        {
            switch (_levelConstructor.LevelName)
            {
                case LevelName.Ghostly: return 15f + _mazeSpawner.MazeWidth * 0.5f;
                case LevelName.TempleKeeper: return 15f + _mazeSpawner.MazeWidth * 0.5f;
                case LevelName.Trapped: return 10f + _mazeSpawner.MazeWidth * 0.5f;
                default: return 0f;
            }
        }
    }
}
