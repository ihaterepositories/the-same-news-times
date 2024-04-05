using Controllers.InGameControllers;
using Models.Items;
using UnityEngine;

namespace Sound
{
    public class GameSoundsPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        [SerializeField] private AudioClip gameOverSound;
        [SerializeField] private AudioClip scorePickedSound;
        [SerializeField] private AudioClip itemPickedSound;
        [SerializeField] private AudioClip itemUsedSound;

        private void Start()
        {
            if (PlayerPrefs.GetInt("PlayGameSounds", 1) == 0)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            LevelFinisher.OnGameFinished += PlayGameOverSound;
            
            GreenScore.OnPicked += PlayScorePickedSound;
            PinkScore.OnPicked += PlayScorePickedSound;
            
            Key.OnPicked += PlayItemPickedSound;
            LifeSaver.OnPicked += PlayItemPickedSound;
            Booster.OnPicked += PlayItemPickedSound;
            
            Inventory.OnKeyUsed += PlayItemUsedSound;
            Inventory.OnLifeSaverUsed += PlayItemUsedSound;
            Inventory.OnBoosterUsed += PlayItemUsedSound;
        }
        
        private void OnDisable()
        {
            LevelFinisher.OnGameFinished -= PlayGameOverSound;
            
            GreenScore.OnPicked -= PlayScorePickedSound;
            PinkScore.OnPicked -= PlayScorePickedSound;
            
            Key.OnPicked -= PlayItemPickedSound;
            LifeSaver.OnPicked -= PlayItemPickedSound;
            Booster.OnPicked -= PlayItemPickedSound;
            
            Inventory.OnKeyUsed -= PlayItemUsedSound;
            Inventory.OnLifeSaverUsed -= PlayItemUsedSound;
            Inventory.OnBoosterUsed -= PlayItemUsedSound;
        }
        
        private void PlayGameOverSound()
        {
            audioSource.PlayOneShot(gameOverSound);
        }
        
        private void PlayScorePickedSound()
        {
            audioSource.PlayOneShot(scorePickedSound);
        }
        
        private void PlayItemPickedSound()
        {
            audioSource.PlayOneShot(itemPickedSound);
        }

        private void PlayItemUsedSound()
        {
            audioSource.PlayOneShot(itemUsedSound);
        }
    }
}
