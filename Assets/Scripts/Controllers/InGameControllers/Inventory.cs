using System;
using Models.Items;
using UI;
using UnityEngine;

namespace Controllers.InGameControllers
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private NotificationText notificationText;
        
        public int KeysCount { get; private set; }
        public int LifeSaversCount { get; private set; }
        public int BoostersCount { get; private set; }
        
        public static event Action OnLifeSaverUsed;
        public static event Action OnBoosterUsed; 

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
            
            Lock.OnUnlockTry += UseKey;
        }
        
        private void OnDisable()
        {
            Key.OnPicked -= AddKey;
            LifeSaver.OnPicked -= AddLifeSaver;
            Booster.OnPicked -= AddBooster;
            
            Lock.OnUnlockTry -= UseKey;
        }

        private void AddKey()
        {
            KeysCount++;
        }
        
        private void AddLifeSaver()
        {
            LifeSaversCount++;
        }
        
        private void AddBooster()
        {
            BoostersCount++;
        }
        
        private void UseKey(Lock lockObject)
        {
            if (KeysCount <= 0)
            {
                notificationText.ShowNotification("no keys left");
                return;
            }
            KeysCount--;
            notificationText.ShowNotification("key used, exit unlocked");
            lockObject.Reset();
        }
        
        private void UseLifeSaver()
        {
            if (LifeSaversCount <= 0)
            {
                notificationText.ShowNotification("no life savers left");
                return;
            }
            
            LifeSaversCount--;
            notificationText.ShowNotification("life saver used, yo`re teleported");
            OnLifeSaverUsed?.Invoke();
        }

        private void UseBooster()
        {
            if (BoostersCount <= 0)
            {
                notificationText.ShowNotification("no boosters left");
                return;
            }

            BoostersCount--;
            notificationText.ShowNotification("booster used, you`re faster for 5 seconds");
            OnBoosterUsed?.Invoke();
        }
    }
}