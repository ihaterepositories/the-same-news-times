using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ghost : MonoBehaviour, IPoolable
{
    [SerializeField] private SpriteRenderer _spriteRender;

    private bool _isHunting;
    private float _angularSpeed = 1f;
    private float _circleRadius = 1f;
    private Vector2 _fixedPoint;
    private float _currentAngle;

    private Coroutine _transparetyAnimationCoroutine;

    public GameObject GameObject => gameObject;

    public event Action<IPoolable> OnDestroyed;
    public static event Action OnCatchedPlayer;

    private void Update()
    {
        /*if (_isHunting)*/ DoCircleMoving();
    }

    private void OnEnable()
    {
        FinishLevelController.OnLevelFinished += Reset;
    }

    private void OnDisable()
    {
        FinishLevelController.OnLevelFinished -= Reset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            OnCatchedPlayer?.Invoke();
        }
    }

    public void Reset()
    {
        StopHunting();
        OnDestroyed?.Invoke(this);
    }

    private void StopHunting()
    {
        _isHunting = false;

        if (_transparetyAnimationCoroutine != null)
        {
            StopCoroutine(_transparetyAnimationCoroutine);
            _transparetyAnimationCoroutine = null;
        }
    }

    public void StartHunting()
    {
        _transparetyAnimationCoroutine = StartCoroutine(TransparetyAnimationCoroutine());
        GenerateCircleMovingParametrs();
        _isHunting = true;
        _fixedPoint = transform.position;
    }

    private IEnumerator TransparetyAnimationCoroutine()
    {
        yield return new WaitForSeconds(1);
        _spriteRender.DOFade(0.3f, 1f);
        yield return new WaitForSeconds(1);
        _spriteRender.DOFade(1f, 1f);
        StartCoroutine(TransparetyAnimationCoroutine());
    }

    private void GenerateCircleMovingParametrs()
    {
        _angularSpeed = Random.Range(0.7f, 1.3f);
        _circleRadius = Random.Range(2f, 4f);
    }

    private void DoCircleMoving()
    {
        _currentAngle += _angularSpeed * Time.deltaTime;
        Vector2 offset = new Vector2(Mathf.Sin(_currentAngle), Mathf.Cos(_currentAngle)) * _circleRadius;
        transform.position = _fixedPoint + offset;
    }
}
