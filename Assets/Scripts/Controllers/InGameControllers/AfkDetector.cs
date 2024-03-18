using System;
using UnityEngine;

namespace Controllers.InGameControllers
{
    public class AfkDetector : MonoBehaviour
    {
        private readonly float _maxAfkTime = 30f;
        private float _currentAfkTime;
        private bool _isAfk;
        
        public static event Action<string> OnAfkDetected;
        public static event Action OnPlayerIsAfk;

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
                    OnAfkDetected?.Invoke($"You are afk, game will be finished in {(int)(_maxAfkTime - _currentAfkTime)} seconds.");
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
