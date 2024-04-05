using Controllers;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace AnimationControllers
{
    public class CircleAnimation : MonoBehaviour
    {
        public static CircleAnimation Instance { get; private set; }
        private ScenesLoader _scenesLoader;

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

        [Inject]
        private void Construct(ScenesLoader scenesLoader)
        {
            _scenesLoader = scenesLoader;
        }
        
        private void OnEnable()
        {
            _scenesLoader.OnSceneLoaded += Decrease;
        }
        
        private void OnDisable()
        {
            _scenesLoader.OnSceneLoaded -= Decrease;
        }

        private void Decrease()
        {
            transform.localScale = new Vector2(50f, 50f);
            transform.DOScale(Vector2.zero, 0.5f);
        }

        public void Decrease(float duration)
        {
            transform.localScale = new Vector2(50f, 50f);
            transform.DOScale(Vector2.zero, duration);
        }

        public void Increase()
        {
            transform.localScale = Vector2.zero;
            transform.DOScale(new Vector2(50f, 50f), 0.5f);
        }

        public void Increase(float duration)
        {
            transform.localScale = Vector2.zero;
            transform.DOScale(new Vector2(50f, 50f), duration);
        }
    }
}
