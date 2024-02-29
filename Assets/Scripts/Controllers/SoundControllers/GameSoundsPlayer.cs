using Models.Items;
using UnityEngine;

namespace Controllers.SoundControllers
{
    public class GameSoundsPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        [SerializeField] private AudioClip gameOverSound;
        [SerializeField] private AudioClip itemPickedSound;
        [SerializeField] private AudioClip levelFinishedSound;
        
        private void OnEnable()
        {
            FinishLevelController.OnGameFinished += PlayGameOverSound;
            GreenScore.OnPicked += PlayItemPickedSound;
            PinkScore.OnPicked += PlayLevelFinishedSound;
        }
        
        private void OnDisable()
        {
            FinishLevelController.OnGameFinished -= PlayGameOverSound;
            GreenScore.OnPicked -= PlayItemPickedSound;
            PinkScore.OnPicked -= PlayLevelFinishedSound;
        }
        
        private void PlayGameOverSound()
        {
            audioSource.PlayOneShot(gameOverSound);
        }
        
        private void PlayItemPickedSound()
        {
            audioSource.PlayOneShot(itemPickedSound);
        }
        
        private void PlayLevelFinishedSound()
        {
            audioSource.PlayOneShot(levelFinishedSound);
        }
    }
}
