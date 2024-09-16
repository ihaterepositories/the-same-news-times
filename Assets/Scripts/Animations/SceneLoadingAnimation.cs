using DG.Tweening;
using Loaders;
using UnityEngine;
using Zenject;

namespace Animations
{
    public class SceneLoadingAnimation : MonoBehaviour
    {
        private SceneLoadingAnimation _instance;
        private ScenesLoader _scenesLoader;

        private void Awake()
        {
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
            transform.localScale = new Vector2(200f, 200f);
            transform.DOScale(Vector2.zero, 0.5f);
        }

        public void Decrease(float duration)
        {
            transform.localScale = new Vector2(200f, 200f);
            transform.DOScale(Vector2.zero, duration);
        }

        public void Increase()
        {
            transform.localScale = Vector2.zero;
            transform.DOScale(new Vector2(200f, 200f), 0.5f);
        }

        public void Increase(float duration)
        {
            transform.localScale = Vector2.zero;
            transform.DOScale(new Vector2(200f, 200f), duration);
        }
    }
}
