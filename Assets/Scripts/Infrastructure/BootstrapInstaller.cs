using Controllers;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<ScenesLoader>()
                .AsSingle();
        }
    }
}