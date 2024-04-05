using Controllers;
using Networking.Requests;
using UI;
using UI.Formatters;
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