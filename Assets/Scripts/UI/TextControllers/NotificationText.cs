using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.TextControllers
{
    public class NotificationText : MonoBehaviour
    {
        private Text _textObject;

        private void Awake()
        {
            _textObject = GetComponent<Text>();
        }

        public void ShowNotification(string text, bool isHideAfterTime = true)
        {
            _textObject.DOFade(1f, 1f);
            _textObject.text = text;
            
            if (isHideAfterTime)
                StartCoroutine(HideNotificationCoroutine());
        }
        
        private IEnumerator HideNotificationCoroutine()
        {
            yield return new WaitForSeconds(5f);
            _textObject.DOFade(0f, 4f);
        }
    }
}
