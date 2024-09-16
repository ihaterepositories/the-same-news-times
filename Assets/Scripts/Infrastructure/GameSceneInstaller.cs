using Animations;
using Controllers.InGameControllers;
using Loaders;
using Models;
using Models.Items;
using Spawners;
using Spawners.EnemiesSpawners;
using Spawners.ItemsSpawners;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private Timer timer;
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
            
            InstallObjectsSpawners();

            Container
                .Bind<ObjectsSpawner>()
                .AsSingle();
            
            Container
                .Bind<LevelConstructor>()
                .AsSingle();
            
            Container
                .Bind<LevelSpawner>()
                .AsSingle();

            Container
                .Bind<Timer>()
                .FromComponentInNewPrefab(timer)
                .AsSingle();
        }

        private void InstallObjectsSpawners()
        {
            Container
                .Bind<MazeExitSpawner>()
                .FromComponentInNewPrefab(_prefabsLoader.GetPrefab("PinkScoreSpawner"))
                .AsSingle();
            
            Container
                .Bind<PointsSpawner>()
                .FromComponentInNewPrefab(_prefabsLoader.GetPrefab("GreenScoresSpawner"))
                .AsSingle();

            Container
                .Bind<ModificatorsSpawner>()
                .FromComponentInNewPrefab(_prefabsLoader.GetPrefab("ModificatorsSpawner"))
                .AsSingle();
            
            Container
                .Bind<TempleKeeperSpawner>()
                .FromComponentInNewPrefab(_prefabsLoader.GetPrefab("TempleKeeperSpawner"))
                .AsSingle();
            
            Container
                .Bind<TrapSpawner>()
                .FromComponentInNewPrefab(_prefabsLoader.GetPrefab("TrapSpawner"))
                .AsSingle();
            
            Container
                .Bind<GhostSpawner>()
                .FromComponentInNewPrefab(_prefabsLoader.GetPrefab("GhostSpawner"))
                .AsSingle();
        }
    }
}