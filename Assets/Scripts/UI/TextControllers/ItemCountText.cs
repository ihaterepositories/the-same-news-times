using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.TextControllers
{
    public class ItemCountText : MonoBehaviour
    {
        [SerializeField] private Text countText;

        public Func<int> GetCount;

        private void Update()
        {
            SetCountText();
        }

        private void SetCountText()
        {
            if (GetCount != null)
            {
                countText.text = GetCount() + "x";
            }
        }
    }
}