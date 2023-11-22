using System;
using UnityEngine;

public class TimeSettingController : MonoBehaviour
{
    [SerializeField] private InfoText _timeSettingText;

    private float _timeDuration;

    public static event Action OnTimeSettingEntered;

    private void Start()
    {
        OnTimeSettingEntered?.Invoke();
        _timeDuration = 60f;
    }

    private void Update()
    {
        ChangeTimeSettings();
        SetTime();
    }

    private void ChangeTimeSettings()
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

    private void SetTime()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            PlayerPrefs.SetFloat("TimerSeconds", _timeDuration);
        }
    }
}
