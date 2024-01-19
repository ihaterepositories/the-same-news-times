using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CellWallsCollector : MonoBehaviour, IPoolable
{
    public GameObject leftWall;
    public GameObject bottomWall;

    public SpriteRenderer leftWallSpriteRenderer;
    public SpriteRenderer bottomWallSpriteRenderer;

    public GameObject GameObject => gameObject;
    public event Action<IPoolable> OnDestroyed;

    private void OnEnable()
    {
        FinishLevelController.OnLevelFinished += Reset;
    }

    private void OnDisable()
    {
        FinishLevelController.OnLevelFinished -= Reset;
    }

    public void Reset()
    {
        OnDestroyed?.Invoke(this);
    }

    public void PlayAppearanceAnimation(float duration)
    {
        bottomWallSpriteRenderer.DOFade(1, duration);
        leftWallSpriteRenderer.DOFade(1, duration);
    }

    public void ChangeTransparety(float transparety)
    {
        bottomWallSpriteRenderer.color = new Color(
                    bottomWallSpriteRenderer.color.r,
                    bottomWallSpriteRenderer.color.g,
                    bottomWallSpriteRenderer.color.b,
                    transparety);

        leftWallSpriteRenderer.color = new Color(
                    bottomWallSpriteRenderer.color.r,
                    bottomWallSpriteRenderer.color.g,
                    bottomWallSpriteRenderer.color.b,
                    transparety);
    }
}
