using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]

public class PressKeyToContinueText : MonoBehaviour
{
    [SerializeField] private KeyCode _keyToPress;
    [SerializeField] private string _nextSceneString;

    private Text _text;
    private float _scaleX;
    private float _scaleY;

    private void Awake()
    {
        _text = GetComponent<Text>();
        _scaleX = _text.rectTransform.localScale.x;
        _scaleY = _text.rectTransform.localScale.y;
    }

    private void Start()
    {
        StartCoroutine(ChangeScaleCoroutine());
    }

    private void Update()
    {
        LoadNextScene();
    }

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

    private void LoadNextScene()
    {
        if (Input.GetKeyDown(_keyToPress))
        {
            CircleAnimation.Instance.Increase(6);

            StartCoroutine(LoadNextSceneCoroutine());
        }
    }

    private IEnumerator LoadNextSceneCoroutine()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(LoadSceneAsyncCoroutine());
    }

    private IEnumerator LoadSceneAsyncCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_nextSceneString);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
