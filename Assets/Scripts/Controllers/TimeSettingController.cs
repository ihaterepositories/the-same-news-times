using System;
using UnityEngine;

public class TimeSettingController : MonoBehaviour
{
    [SerializeField] private InfoText _timeSettingText;

    private float _timeDuration;

    private void Start()
    {
        CircleAnimation.Instance.Decrease();

        _timeDuration = 60f;
    }

    private void Update()
    {
        GetTimeSettingsFromUser();
        SaveSettedTime();
    }

    private void GetTimeSettingsFromUser()
    {
        _timeSettingText.SetText(TimeSpan.FromSeconds(_timeDuration).ToString());

        if (Input.GetKeyDown(KeyCode.W))
        {
            _timeDuration+=60f;
        }
        else if (Input.GetKeyDown(KeyCode.S) && _timeDuration > 119f)
        {
            _timeDuration-=60;
        }
    }

    private void SaveSettedTime()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            PlayerPrefs.SetFloat("TimerSeconds", _timeDuration);
        }
    }
}
