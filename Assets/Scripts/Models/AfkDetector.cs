using System;
using System.Collections;
using UnityEngine;

namespace Models
{
    public class AfkDetector : MonoBehaviour
    {
        private Vector3 _lastPosition;
        private bool _isMoving;
        private readonly float _movementThreshold = 0.01f;
        private Coroutine _detectionCoroutine;

        public static event Action<string> OnAfkDetected; 
        public static event Action OnObjectIsAfk; 

        private void OnEnable()
        {
            StartLevelController.OnAllSpawned += DoDetection;
            FinishLevelController.OnLevelFinished += StopDetection;
        }

        private void OnDisable()
        {
            StartLevelController.OnAllSpawned -= DoDetection;
            FinishLevelController.OnLevelFinished -= StopDetection;
        }

        private void StopDetection()
        {
            if (_detectionCoroutine != null)
            {
                StopCoroutine(_detectionCoroutine);
                _detectionCoroutine = null;
            }
        }
        
        private void DoDetection()
        {
            _detectionCoroutine = StartCoroutine(DoDetectionCoroutine());
        }
        
        private IEnumerator DoDetectionCoroutine()
        {
            yield return new WaitForSeconds(30f);
            
            _isMoving = Vector3.Distance(transform.position, _lastPosition) > _movementThreshold;
            _lastPosition = transform.localPosition;

            StartCoroutine(_isMoving ? DoDetectionCoroutine() : AfkNotifyingCoroutine());
        }

        private IEnumerator AfkNotifyingCoroutine()
        {
            OnAfkDetected?.Invoke("You are AFK, game will be finished soon...");
            yield return new WaitForSeconds(10f);
            OnObjectIsAfk?.Invoke();
        }
    }
}
