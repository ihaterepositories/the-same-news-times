using System.Collections;
using AnimationControllers;
using UnityEngine;

namespace UI
{
    public class FinishGameButton : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            CircleAnimation.Instance.Increase();
            StartCoroutine(ExitGameCoroutine());
        }

        private IEnumerator ExitGameCoroutine()
        {
            yield return new WaitForSeconds(1f);
            QuitGame();
        }

        private void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
