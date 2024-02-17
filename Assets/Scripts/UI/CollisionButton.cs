using Controllers;
using UnityEngine;

namespace UI
{
    public class CollisionButton : MonoBehaviour
    {
        [SerializeField] private string nextSceneName;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            SceneLoadingController.Instance.LoadSceneAsync(nextSceneName);
        }
    }
}
