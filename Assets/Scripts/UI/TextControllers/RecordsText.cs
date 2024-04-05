using UI.Formatters;
using UnityEngine;
using UnityEngine.UI;

namespace UI.TextControllers
{
    public class RecordsText : MonoBehaviour
    {
        [SerializeField] private Text recordsText;

        private void Start()
        {
            recordsText.text = GetLocalRecords();
        }
        
        private string GetLocalRecords()
        {
            return
                "Best pink score: " + $"{PlayerPrefs.GetInt("BestPinkScore", 0)}" + "\n" +
                "Best green score: " + $"{PlayerPrefs.GetInt("BestGreenScore", 0)}" + "\n" +
                "Best total score: " + $"{PlayerPrefs.GetInt("BestTotalScore", 0)}" + "\n" +
                "Total mazes completed: " + $"{PlayerPrefs.GetInt("TotalMazesCompleted", 0)}" + "\n" +
                "Longest game: " + $"{TimeFormatter.Format(PlayerPrefs.GetFloat("BestGameDuration", 0f))}" + "\n" +
                "Total time spent in temples: " + $"{TimeFormatter.Format(PlayerPrefs.GetFloat("TotalTimeSpent", 0f))}"
                ;
        }
    }
}