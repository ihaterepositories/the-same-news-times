using UnityEngine;

public class RecordsSceneController : MonoBehaviour
{
    [SerializeField] private InfoText recordsText;

    private void Start()
    {
        recordsText.SetText(GetLocalRecords());
        CircleAnimation.Instance.Decrease();
    }

    private string GetLocalRecords()
    {
        return
            "Best green score: " + $"{PlayerPrefs.GetInt("BestGreenScore", 0)}" + "\n" +
            "Best pink score: " + $"{PlayerPrefs.GetInt("BestPinkScore", 0)}" + "\n" +
            "Best total score: " + $"{PlayerPrefs.GetInt("BestTotalScore", 0)}" + "\n" +
            "Total mazes completed: " + $"{PlayerPrefs.GetInt("TotalMazesCompleted", 0)}" + "\n"
            ;
    }
}
