using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sound
{
    public class MainSoundtrackPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource soundtrack;
        
        private MainSoundtrackPlayer _instance;
        private float _startVolume;
        
        private void Awake()
        {
            _startVolume = soundtrack.volume;
            
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            soundtrack.Play();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (PlayerPrefs.GetInt("PlaySoundtracks", 1) == 0)
            {
                soundtrack.volume = 0;
            }
            else
            {
                soundtrack.volume = _startVolume;
            }
            
            if (scene.name != "GameScene")
            {
                if (!soundtrack.isPlaying) soundtrack.Play();
            }
            else
            {
                FadeOutMusic(soundtrack, 0.5f);
            }
        }
        
        private void FadeOutMusic(AudioSource audioSource, float fadeDuration)
        {
            StartCoroutine(FadeOutCoroutine(audioSource, fadeDuration));
        }

        private IEnumerator FadeOutCoroutine(AudioSource audioSource, float fadeDuration)
        {
            var startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume;
        }
    }
}
