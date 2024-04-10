using System;
using UI.TextControllers;
using UnityEngine;
using Zenject;

namespace Controllers.InGameControllers
{
    public class AfkDetector : MonoBehaviour
    {
        private readonly float _maxAfkTime = 30f;
        private float _currentAfkTime;
        private bool _isAfk;
        private NotificationText _notificationText;

        public static event Action OnPlayerIsAfk;
        
        [Inject]
        private void Construct(NotificationText notificationText)
        {
            _notificationText = notificationText;
        }

        private void Update()
        {
            DoAfkDetection();
        }

        private void DoAfkDetection()
        {
            if (Input.anyKey)
            {
                _currentAfkTime = 0f;
                if (_isAfk) _isAfk = false;
            }
            else
            {
                _currentAfkTime += Time.deltaTime;

                if (_currentAfkTime >= 15f)
                {
                    _notificationText.ShowNotification($"You are afk, game will be finished in {(int)(_maxAfkTime - _currentAfkTime)} seconds.", false);
                }
                
                if (_currentAfkTime >= _maxAfkTime && !_isAfk)
                {
                    _isAfk = true;
                    OnPlayerIsAfk?.Invoke();   
                }
            }
        }
    }
}
