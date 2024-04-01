using Controllers;
using Requests;
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

            Container
                .Bind<BestPlayerRequest>()
                .AsSingle();
            
            Container
                .Bind<BestPlayersRequest>()
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