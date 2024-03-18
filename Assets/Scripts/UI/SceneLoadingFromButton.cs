using System.Collections;
using AnimationsScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class SceneLoadingFromButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private string sceneName;
        
        private void Start()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            StartCoroutine(LoadSceneWithAnimationCoroutine());
        }

        private IEnumerator LoadSceneWithAnimationCoroutine()
        {
            CircleAnimation.Instance.Increase();
            yield return new WaitForSeconds(1f);
            StartCoroutine(LoadSceneAsyncCoroutine());
        }

        private IEnumerator LoadSceneAsyncCoroutine()
        {
            var asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}
