using Animations;
using Controllers.InGameControllers;
using Loaders;
using Models;
using Spawners;
using Spawners.EnemiesSpawners;
using Spawners.ItemsSpawners;
using Spawners.LevelsSpawners;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private AfkDetector afkDetector;
        
        private PrefabsLoader _prefabsLoader;
        
        [Inject]
        private void Construct(PrefabsLoader prefabsLoader)
        {
            _prefabsLoader = prefabsLoader;
        }
        
        public override void InstallBindings()
        {
            Container
                .Bind<MazeDisappearanceAnimation>()
                .FromComponentInNewPrefab(_prefabsLoader.GetPrefab("MazeAppearanceAnimation"))
                .AsSingle();
            
            Container
                .Bind<MazeSpawner>()
                .FromComponentInNewPrefab(_prefabsLoader.GetPrefab("MazeSpawner"))
                .AsSingle();
            
            Container
                .Bind<Player>()
                .FromComponentInNewPrefab(_prefabsLoader.GetPrefab("Player"))
                .AsSingle();
            
            Container
                .Bind<PositionsBlocker>()
                .FromComponentInNewPrefab(_prefabsLoader.GetPrefab("PositionsBlocker"))
                .AsSingle();
            
            Container
                .Bind<AfkDetector>()
                .FromInstance(afkDetector)
                .AsSingle();
            
            InstallObjectsSpawners();

            Container
                .Bind<ObjectsSpawner>()
                .AsSingle();
            
            InstallLevelSpawner();
            
            Container
                .Bind<LevelSpawner>()
                .AsSingle();

            Container
                .Bind<Inventory>()
                .FromInstance(inventory)
                .AsSingle();
        }

        private void InstallObjectsSpawners()
        {
            Container
                .Bind<PinkScoreSpawner>()
                .FromComponentInNewPrefab(_prefabsLoader.GetPrefab("PinkScoreSpawner"))
                .AsSingle();
            
            Container
                .Bind<GreenScoresSpawner>()
                .FromComponentInNewPrefab(_prefabsLoader.GetPrefab("GreenScoresSpawner"))
                .AsSingle();
            
            Container
                .Bind<TempleKeeperSpawner>()
                .FromComponentInNewPrefab(_prefabsLoader.GetPrefab("TempleKeeperSpawner"))
                .AsSingle();
            
            Container
                .Bind<LockSpawner>()
                .FromComponentInNewPrefab(_prefabsLoader.GetPrefab("LockSpawner"))
                .AsSingle();
            
            Container
                .Bind<KeySpawner>()
                .FromComponentInNewPrefab(_prefabsLoader.GetPrefab("KeySpawner"))
                .AsSingle();
            
            Container
                .Bind<TrapSpawner>()
                .FromComponentInNewPrefab(_prefabsLoader.GetPrefab("TrapSpawner"))
                .AsSingle();
            
            Container
                .Bind<GhostSpawner>()
                .FromComponentInNewPrefab(_prefabsLoader.GetPrefab("GhostSpawner"))
                .AsSingle();

            Container
                .Bind<BoosterSpawner>()
                .FromComponentInNewPrefab(_prefabsLoader.GetPrefab("BoosterSpawner"))
                .AsSingle();

            Container
                .Bind<LifeSaverSpawner>()
                .FromComponentInNewPrefab(_prefabsLoader.GetPrefab("LifeSaverSpawner"))
                .AsSingle();
            
            Container
                .Bind<PoisonSpawner>()
                .FromComponentInNewPrefab(_prefabsLoader.GetPrefab("PoisonSpawner"))
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