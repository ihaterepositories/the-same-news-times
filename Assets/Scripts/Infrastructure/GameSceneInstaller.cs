using AnimationControllers;
using Models;
using Spawners;
using Spawners.ItemsSpawners;
using Spawners.LevelsSpawners;
using Spawners.ObjectsSpawners;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject mazeSpawnerPrefab;
        [SerializeField] private GameObject pinkScoreSpawnerPrefab;
        [SerializeField] private GameObject greenScoresSpawnerPrefab;
        [SerializeField] private GameObject templeKeeperSpawnerPrefab;
        [SerializeField] private GameObject lockSpawnerPrefab;
        [SerializeField] private GameObject keySpawnerPrefab;
        [SerializeField] private GameObject trapSpawnerPrefab;
        [SerializeField] private GameObject ghostSpawnerPrefab;
        [SerializeField] private GameObject boosterSpawnerPrefab;
        [SerializeField] private GameObject lifeSaverSpawnerPrefab;
        [SerializeField] private GameObject mazeAppearanceAnimationPrefab;
        
        public override void InstallBindings()
        {
            Container
                .Bind<MazeAppearanceAnimation>()
                .FromComponentInNewPrefab(mazeAppearanceAnimationPrefab)
                .AsSingle();
            
            Container
                .Bind<MazeSpawner>()
                .FromComponentInNewPrefab(mazeSpawnerPrefab)
                .AsSingle();
            
            Container
                .Bind<Player>()
                .FromComponentInNewPrefab(playerPrefab)
                .AsSingle();
            
            InstallObjectsSpawners();

            Container
                .Bind<ObjectsSpawner>()
                .AsSingle();
            
            InstallLevelSpawner();
            
            Container
                .Bind<LevelSpawner>()
                .AsSingle();
        }

        private void InstallObjectsSpawners()
        {
            Container
                .Bind<PinkScoreSpawner>()
                .FromComponentInNewPrefab(pinkScoreSpawnerPrefab)
                .AsSingle();
            
            Container
                .Bind<GreenScoresSpawner>()
                .FromComponentInNewPrefab(greenScoresSpawnerPrefab)
                .AsSingle();
            
            Container
                .Bind<TempleKeeperSpawner>()
                .FromComponentInNewPrefab(templeKeeperSpawnerPrefab)
                .AsSingle();
            
            Container
                .Bind<LockSpawner>()
                .FromComponentInNewPrefab(lockSpawnerPrefab)
                .AsSingle();
            
            Container
                .Bind<KeySpawner>()
                .FromComponentInNewPrefab(keySpawnerPrefab)
                .AsSingle();
            
            Container
                .Bind<TrapSpawner>()
                .FromComponentInNewPrefab(trapSpawnerPrefab)
                .AsSingle();
            
            Container
                .Bind<GhostSpawner>()
                .FromComponentInNewPrefab(ghostSpawnerPrefab)
                .AsSingle();

            Container
                .Bind<BoosterSpawner>()
                .FromComponentInNewPrefab(boosterSpawnerPrefab)
                .AsSingle();

            Container
                .Bind<LifeSaverSpawner>()
                .FromComponentInNewPrefab(lifeSaverSpawnerPrefab)
                .AsSingle();
        }

        private void InstallLevelSpawner()
        {
            Container
                .Bind<DefaultLevelsSpawner>()
                .AsSingle();
            
            Container
                .Bind<RareLevelsSpawner>()
                .AsSingle();
            
            Container
                .Bind<EpicLevelsSpawner>()
                .AsSingle();
            
            Container
                .Bind<LegendaryLevelsSpawner>()
                .AsSingle();
        }
    }
}