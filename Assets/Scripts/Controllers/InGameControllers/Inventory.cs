using System;
using Models;
using Models.Items;
using UI.TextControllers;
using UnityEngine;
using Zenject;

namespace Controllers.InGameControllers
{
    public class Inventory : MonoBehaviour
    {
        private NotificationText _notificationText;
        private Player _player;
        
        public int KeysCount { get; private set; }
        public int LifeSaversCount { get; private set; }
        public int BoostersCount { get; private set; }
        
        public static event Action OnKeyUsed;
        public static event Action OnLifeSaverUsed;
        public static event Action OnItemUsed;

        [Inject]
        private void Construct(NotificationText notificationText, Player player)
        {
            _notificationText = notificationText;
            _player = player;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                UseLifeSaver();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                UseBooster();
            }
        }

        private void OnEnable()
        {
            Key.OnPicked += AddKey;
            LifeSaver.OnPicked += AddLifeSaver;
            Booster.OnPicked += AddBooster;
            
            Poison.OnPicked += UsePoison;
            Lock.OnUnlockTry += UseKey;
        }
        
        private void OnDisable()
        {
            Key.OnPicked -= AddKey;
            LifeSaver.OnPicked -= AddLifeSaver;
            Booster.OnPicked -= AddBooster;
            
            Poison.OnPicked -= UsePoison;
            Lock.OnUnlockTry -= UseKey;
        }

        private void AddKey() => KeysCount++;
        private void AddLifeSaver() => LifeSaversCount++;
        private void AddBooster() => BoostersCount++;
        
        private void UseKey(Lock lockObject)
        {
            if (KeysCount <= 0)
            {
                _notificationText.ShowNotification("no keys left");
                return;
            }
            
            KeysCount--;
            _notificationText.ShowNotification("key used, exit unlocked");
            lockObject.Reset();
            OnKeyUsed?.Invoke();
            OnItemUsed?.Invoke();
        }
        
        private void UseLifeSaver()
        {
            if (LifeSaversCount <= 0)
            {
                _notificationText.ShowNotification("no life savers left");
                return;
            }
            
            LifeSaversCount--;
            _notificationText.ShowNotification("life saver used, yo`re teleported");
            OnLifeSaverUsed?.Invoke();
            OnItemUsed?.Invoke();
        }

        private void UseBooster()
        {
            if (BoostersCount <= 0)
            {
                _notificationText.ShowNotification("no boosters left");
                return;
            }

            BoostersCount--;
            _notificationText.ShowNotification("booster used, you`re faster for some time");
            StartCoroutine(_player.BoostCoroutine());
            OnItemUsed?.Invoke();
        }
        
        private void UsePoison()
        {
            if (_player.IsBoosted)
            {
                _notificationText.ShowNotification("poison drunk, but booster saved you");
                return; 
            }
            
            _notificationText.ShowNotification("poison drunk, you`re slower for some time");
            StartCoroutine(_player.SlowDownCoroutine());
        }
    }
}