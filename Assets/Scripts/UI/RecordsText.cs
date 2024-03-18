using Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RecordsText : MonoBehaviour
    {
        [SerializeField] private Text recordsText;

        private void Start()
        {
            recordsText.text = RecordsProvider.GetLocalRecords();
        }
    }
}