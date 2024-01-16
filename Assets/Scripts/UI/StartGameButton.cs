using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class StartGameButton : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPrefs.SetInt("GreenScore", 0);
        PlayerPrefs.SetInt("PinkScore", 0);

        CircleAnimation.Instance.Increase(4);

        StartCoroutine(LoadKeyTipsSceneCoroutine());
    }

    private IEnumerator LoadKeyTipsSceneCoroutine()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(LoadKeyTipsSceneCoroutineAsync());
    }

    private IEnumerator LoadKeyTipsSceneCoroutineAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("KeyTipsScene");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
