using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Controllers
{
    public class OptionsController : MonoBehaviour
    {
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color unselectedColor;
        
        [SerializeField] private Text soundtracksText;
        [SerializeField] private Text gameSoundsText;
        
        private void Start()
        {
            soundtracksText.color = PlayerPrefs.GetInt("PlaySoundtracks", 1) == 1 ? selectedColor : unselectedColor;
            gameSoundsText.color = PlayerPrefs.GetInt("PlayGameSounds", 1) == 1 ? selectedColor : unselectedColor;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                PlayerPrefs.SetInt("PlaySoundtracks", PlayerPrefs.GetInt("PlaySoundtracks", 1) == 1 ? 0 : 1);
                soundtracksText.color = PlayerPrefs.GetInt("PlaySoundtracks", 1) == 1 ? selectedColor : unselectedColor;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                PlayerPrefs.SetInt("PlayGameSounds", PlayerPrefs.GetInt("PlayGameSounds", 1) == 1 ? 0 : 1);
                gameSoundsText.color = PlayerPrefs.GetInt("PlayGameSounds", 1) == 1 ? selectedColor : unselectedColor;
            }
        }
    }
}
