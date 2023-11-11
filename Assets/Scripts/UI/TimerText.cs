using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TimerText : MonoBehaviour
{
    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        Timer.OnTimerWorking += ShowTime;
    }

    private void OnDisable()
    {
        Timer.OnTimerWorking -= ShowTime;
    }

    private string ConvertTimeToString(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        return formattedTime;
    }

    private void ShowTime(float time)
    {
        _text.text = ConvertTimeToString(time);
    }
}
