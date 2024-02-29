using System.Collections;
using UnityEngine;

namespace Controllers.SoundControllers
{
    public class GameSoundtracksPlayer : MonoBehaviour
    {
        [SerializeField] public AudioClip[] soundtracks;
        [SerializeField] private AudioSource audioSource;

        private float _startVolume;
        private bool _isGameOver;
        private int _lastTrackIndex = -1;
        
        private void Start()
        {
            _startVolume = audioSource.volume;
            
            if (soundtracks.Length > 0)
            {
                PlayNextTrack();
            }
        }

        private void Update()
        {
            if (!audioSource.isPlaying && !_isGameOver)
            {
                PlayNextTrack();
            }
        }

        private void OnEnable()
        {
            FinishLevelController.OnGameFinished += FadeOutMusic;
        }
        
        private void OnDisable()
        {
            FinishLevelController.OnGameFinished -= FadeOutMusic;
        }

        private void PlayNextTrack()
        {
            var trackIndex = Random.Range(0, soundtracks.Length);
            
            while (trackIndex == _lastTrackIndex)
            {
                trackIndex = Random.Range(0, soundtracks.Length);
            }
            
            _lastTrackIndex = trackIndex;
            audioSource.clip = soundtracks[trackIndex];
            audioSource.Play();
        }
        
        private void FadeOutMusic()
        {
            _isGameOver = true;
            StartCoroutine(FadeOutCoroutine(1f));
        }

        private IEnumerator FadeOutCoroutine(float fadeDuration)
        {
            while (audioSource.volume > 0)
            {
                audioSource.volume -= _startVolume * Time.deltaTime / fadeDuration;
                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = _startVolume;
        }
    }
}
