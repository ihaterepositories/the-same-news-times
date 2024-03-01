using System;
using Models.Items;
using UnityEngine;

namespace Controllers.SoundControllers
{
    public class GameSoundsPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        [SerializeField] private AudioClip gameOverSound;
        [SerializeField] private AudioClip itemPickedSound;
        [SerializeField] private AudioClip toolPickedSound;

        private void Start()
        {
            if (PlayerPrefs.GetInt("PlayGameSounds", 1) == 0)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            FinishLevelController.OnGameFinished += PlayGameOverSound;
            GreenScore.OnPicked += PlayItemPickedSound;
            PinkScore.OnPicked += PlayItemPickedSound;
            Key.OnPicked += PlayToolPickedSound;
        }
        
        private void OnDisable()
        {
            FinishLevelController.OnGameFinished -= PlayGameOverSound;
            GreenScore.OnPicked -= PlayItemPickedSound;
            PinkScore.OnPicked -= PlayItemPickedSound;
            Key.OnPicked -= PlayToolPickedSound;
        }
        
        private void PlayGameOverSound()
        {
            audioSource.PlayOneShot(gameOverSound);
        }
        
        private void PlayItemPickedSound()
        {
            audioSource.PlayOneShot(itemPickedSound);
        }
        
        private void PlayToolPickedSound()
        {
            audioSource.PlayOneShot(toolPickedSound);
        }
    }
}
