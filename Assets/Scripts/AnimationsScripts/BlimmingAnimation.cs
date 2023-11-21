using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BlimmingAnimation : MonoBehaviour
{
    [SerializeField] private Color _colorToBlim;
    [SerializeField] private float _blimDuration;

    private SpriteRenderer _spriteRenderer;
    private Color _baseColor;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _baseColor = _spriteRenderer.color;
    }

    private void Start()
    {
        StartCoroutine(BlimmingCoroutine());
    }

    private IEnumerator BlimmingCoroutine()
    {
        _spriteRenderer.color = _baseColor;
        yield return new WaitForSeconds(_blimDuration);
        _spriteRenderer.color = _colorToBlim;
        yield return new WaitForSeconds(_blimDuration);
        StartCoroutine(BlimmingCoroutine());
    }
}
