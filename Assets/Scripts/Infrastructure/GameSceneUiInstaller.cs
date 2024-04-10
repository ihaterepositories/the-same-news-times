using UI.TextControllers;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameSceneUiInstaller : MonoInstaller
    {
        [SerializeField] private NotificationText _notificationText;
        
        public override void InstallBindings()
        {
            Container
                .Bind<NotificationText>()
                .FromInstance(_notificationText)
                .AsSingle();
        }
    }
}