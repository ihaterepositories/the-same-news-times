using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionButton : MonoBehaviour
{
    [SerializeField] private string nextSceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneLoadingController.Instance.LoadSceneAsync(nextSceneName);
    }
}
