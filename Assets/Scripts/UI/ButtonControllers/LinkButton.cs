using UnityEngine;
using UnityEngine.UI;

namespace UI.ButtonControllers
{
    public class LinkButton : MonoBehaviour
    {
        [SerializeField] private string link;
        [SerializeField] private Button button;
        
        private void Awake()
        {
            button.onClick.AddListener(() => Application.OpenURL(link));
        }
    }
}