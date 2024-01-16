using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CircleAnimation : MonoBehaviour
{
    private static CircleAnimation _instance;
    private SpriteRenderer _spriteRenderer;

    public int SortingOrder { set { _spriteRenderer.sortingOrder = value; } }
    public static CircleAnimation Instance {  get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Decrease();
    }

    public void Decrease()
    {
        transform.localScale = new Vector2(50f, 50f);
        transform.DOScale(Vector2.zero, 1f);
    }

    public void Increase()
    {
        transform.localScale = Vector2.zero;
        transform.DOScale(new Vector2(50f, 50f), 1f);
    }
}
