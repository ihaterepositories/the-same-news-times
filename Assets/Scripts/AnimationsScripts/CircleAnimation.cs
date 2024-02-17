using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AnimationsScripts
{
    public class CircleAnimation : MonoBehaviour
    {
        public static CircleAnimation Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Decrease();
        }

        private void Decrease()
        {
            transform.localScale = new Vector2(50f, 50f);
            transform.DOScale(Vector2.zero, 1f);
        }

        public void Decrease(float duration)
        {
            transform.localScale = new Vector2(50f, 50f);
            transform.DOScale(Vector2.zero, duration);
        }

        public void Increase()
        {
            transform.localScale = Vector2.zero;
            transform.DOScale(new Vector2(50f, 50f), 1f);
        }

        public void Increase(float duration)
        {
            transform.localScale = Vector2.zero;
            transform.DOScale(new Vector2(50f, 50f), duration);
        }
    }
}
