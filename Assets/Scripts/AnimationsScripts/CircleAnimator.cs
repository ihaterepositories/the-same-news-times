using DG.Tweening;
using UnityEngine;

public class CircleAnimator : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public int SortingOrder { get { return _spriteRenderer.sortingOrder; } set { _spriteRenderer.sortingOrder = value; } } 

    public void DecreaseCircle()
    {
        transform.localScale = new Vector2(50f, 50f);
        transform.DOScale(Vector2.zero, 1f);
    }

    public void IncreaseCircle()
    {
        transform.localScale = Vector2.zero;
        transform.DOScale(new Vector2(50f, 50f), 1f);
    }
}
