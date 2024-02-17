using System.Collections;
using DG.Tweening;
using Models;
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
            AfkDetector.OnAfkDetected += SetText;
        }

        private void OnDisable()
        {
            AfkDetector.OnAfkDetected -= SetText;
        }

        private void SetText(string text)
        {
            _textObject.DOFade(1f, 1f);
            _textObject.text = text;
        }
    }
}
