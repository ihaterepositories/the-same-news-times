using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ExitGameButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        
        private void Start()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}
