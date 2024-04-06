using System.Collections;
using DG.Tweening;
using Loaders;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.TextControllers
{
    [RequireComponent(typeof(Text))]

    public class PressKeyToContinueText : MonoBehaviour
    {
        [SerializeField] private KeyCode keyToPress;
        [SerializeField] private string nextSceneAddress;
        [SerializeField] private bool useCircleAnimation = true;

        private Text _text;
        private float _scaleX;
        private float _scaleY;
        private ScenesLoader _scenesLoader;

        [Inject]
        private void Construct(ScenesLoader scenesLoader)
        {
            _scenesLoader = scenesLoader;
        }
        
        private void Awake()
        {
            _text = GetComponent<Text>();
            var localScale = _text.rectTransform.localScale;
            _scaleX = localScale.x;
            _scaleY = localScale.y;
        }

        private void Update()
        {
            EnableNextSceneLoading();
        }

        private void OnEnable()
        {
            StartCoroutine(ChangeScaleCoroutine());
        }

        // ReSharper disable once FunctionRecursiveOnAllPaths
        private IEnumerator ChangeScaleCoroutine()
        {
            _text.rectTransform.DOScaleX(_scaleX, 0.5f);
            _text.rectTransform.DOScaleY(_scaleY, 0.5f);
            yield return new WaitForSeconds(0.7f);
            _text.rectTransform.DOScaleX(_scaleX / 1.2f, 0.5f);
            _text.rectTransform.DOScaleY(_scaleY / 1.2f, 0.5f);
            yield return new WaitForSeconds(0.7f);
            StartCoroutine(ChangeScaleCoroutine());
        }
        
        private void EnableNextSceneLoading()
        {
            if (Input.GetKeyDown(keyToPress))
            {
                StartCoroutine(_scenesLoader.LoadSceneCoroutine(nextSceneAddress, useCircleAnimation));
            }
        }
    }
}
