using System.Collections;
using AnimationsScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class SceneLoadingController : MonoBehaviour
    {
        public static SceneLoadingController Instance { get; private set; }

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

        public void LoadSceneAsync(string sceneName, bool isAnimated = true)
        {
            StartCoroutine(LoadSceneWithAnimationCoroutine(sceneName, isAnimated));
        }

        private IEnumerator LoadSceneWithAnimationCoroutine(string sceneName, bool isAnimated)
        {
            if (isAnimated) { CircleAnimation.Instance.Increase(); }
            yield return new WaitForSeconds(1f);
            StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
        }

        private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
        {
            var asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}
