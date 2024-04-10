using Animations;
using Loaders;
using Networking.Requests;
using UI.Formatters;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _sceneLoadingAnimation;
        
        public override void InstallBindings()
        {
            Container
                .Bind<ScenesLoader>()
                .AsSingle();
            
            Container
                .Bind<SceneLoadingAnimation>()
                .FromComponentInNewPrefab(_sceneLoadingAnimation)
                .AsSingle();
            
            Container
                .Bind<PrefabsLoader>()
                .AsSingle();
            
            Container
                .Bind<BestPlayersRequest>()
                .AsSingle();

            Container
                .Bind<ScoresFormatter>()
                .AsSingle();
            
            Container
                .Bind<LoginPlayerRequest>()
                .AsSingle();

            Container
                .Bind<RegisterPlayerRequest>()
                .AsSingle();

            Container
                .Bind<UpdateBestRecordRequest>()
                .AsSingle();
        }
    }
}