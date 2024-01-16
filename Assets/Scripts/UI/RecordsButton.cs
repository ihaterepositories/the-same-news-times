using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecordsButton : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CircleAnimation.Instance.Increase(4);
        StartCoroutine(LoadRecordsSceneCoroutine());
    }

    private IEnumerator LoadRecordsSceneCoroutine()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(LoadRecordsSceneCoroutineAsync());
    }

    private IEnumerator LoadRecordsSceneCoroutineAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("RecordsScene");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
