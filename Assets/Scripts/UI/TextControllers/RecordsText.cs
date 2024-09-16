using UnityEngine;
using UnityEngine.UI;

namespace UI.TextControllers
{
    public class RecordsText : MonoBehaviour
    {
        [SerializeField] private Text recordsText;

        private void Start()
        {
            recordsText.text = GetRecords();
        }
        
        private string GetRecords()
        {
            return
                "Best score: " + $"{PlayerPrefs.GetInt("BestScore", -1)}" + "\n" +
                "Total mazes completed: " + $"{PlayerPrefs.GetInt("TotalMazesCompleted", -1)}";
        }
    }
}