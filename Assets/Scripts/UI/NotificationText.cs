using Controllers.InGameControllers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class NotificationText : MonoBehaviour
    {
        private Text _textObject;

        private void Awake()
        {
            _textObject = GetComponent<Text>();
        }

        private void OnEnable()
        {
            AfkDetector.OnAfkDetected += ShowNotification;
            LevelStarter.OnAllSpawned += HideNotification;
        }

        private void OnDisable()
        {
            AfkDetector.OnAfkDetected -= ShowNotification;
            LevelStarter.OnAllSpawned -= HideNotification;
        }

        public void ShowNotification(string text)
        {
            _textObject.DOFade(1f, 1f);
            _textObject.text = text;
        }
        
        private void HideNotification()
        {
            _textObject.DOFade(0f, 1.5f);
        }
    }
}
