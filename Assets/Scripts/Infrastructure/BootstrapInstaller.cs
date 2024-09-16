using Animations;
using Loaders;
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
        }
    }
}