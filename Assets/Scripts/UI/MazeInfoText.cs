using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MazeInfoText : MonoBehaviour
{
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        StartLevelController.OnMazeGenerated += ShowText;
    }

    private void OnDisable()
    {
        StartLevelController.OnMazeGenerated -= ShowText;
    }

    public void ShowText(int mazeSize, int cyclesCount, int eatablePointsCount)
    {
        text.text = $"maze size: {mazeSize}x{mazeSize}  /  cycles: {cyclesCount}  /  eatable points: {eatablePointsCount}";
    }
}
