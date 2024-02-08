using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ghost : MonoBehaviour, IPoolable
{
    [SerializeField] private SpriteRenderer _spriteRender;

    private float maxXPosition;
    private float maxYPosition;
    private float minXPosition;
    private float minYPosition;

    public GameObject GameObject => gameObject;

    public event Action<IPoolable> OnDestroyed;
    public static event Action OnCatchedPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            player.Reset();
            OnCatchedPlayer?.Invoke();
        }
    }

    public void Reset()
    {
        OnDestroyed?.Invoke(this);
    }

    public void StartHunting()
    {
        StartCoroutine(TransparetyAnimationCoroutine());
        StartCoroutine(MovingCoroutine());
    }

    private IEnumerator TransparetyAnimationCoroutine()
    {
        yield return new WaitForSeconds(1);
        _spriteRender.DOFade(0.3f, 1f);
        yield return new WaitForSeconds(1);
        _spriteRender.DOFade(1f, 1f);
        StartCoroutine(TransparetyAnimationCoroutine());
    }

    private IEnumerator MovingCoroutine()
    {
        var newPosition = new Vector2(Random.Range(minXPosition, maxXPosition), Random.Range(minYPosition, maxYPosition));
        yield return new WaitForSeconds(1);
        transform.position = Vector3.Lerp(transform.position, newPosition, 1f * Time.deltaTime);
        yield return new WaitForSeconds(3f);
        StartCoroutine(MovingCoroutine());
    }

    public void SetMaxPositions(float maxXPosition, float maxYPosition)
    {
        this.maxXPosition = maxXPosition;
        this.maxYPosition = maxYPosition;
    }

    public void SetMinPositions(float minXPosition, float minYPosition)
    {
        this.minXPosition = minXPosition;
        this.minYPosition = minYPosition;
    }
}
