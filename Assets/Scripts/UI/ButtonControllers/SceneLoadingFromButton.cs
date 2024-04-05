using Controllers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.ButtonControllers
{
    public class SceneLoadingFromButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private string nextSceneAddress;
        private ScenesLoader _scenesLoader;

        [Inject]
        private void Construct(ScenesLoader scenesLoader)
        {
            _scenesLoader = scenesLoader;
        }
        
        private void Start()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            StartCoroutine(_scenesLoader.LoadSceneCoroutine(nextSceneAddress, true));
        }
    }
}
