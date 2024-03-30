using System.Collections;
using AnimationsScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class ScenesLoader
    {
        public IEnumerator LoadSceneCoroutine(string sceneName, bool isAnimated)
        {
            if (isAnimated) { CircleAnimation.Instance.Increase(); }
            
            yield return new WaitForSeconds(1f);
            
            var asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}
