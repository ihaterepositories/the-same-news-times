using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingController : MonoBehaviour
{
    private static SceneLoadingController _instance;

    public static SceneLoadingController Instance {  get { return _instance; } }

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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
