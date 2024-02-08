using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class GreenScore : MonoBehaviour, IPickable, IPoolable
{
    public GameObject GameObject => gameObject;

    public static event Action OnPicked;
    public event Action<IPoolable> OnDestroyed;

    private void OnEnable()
    {
        FinishLevelController.OnLevelFinished += Reset;
    }

    private void OnDisable()
    {
        FinishLevelController.OnLevelFinished -= Reset;
    }

    public void Pick()
    {
        OnPicked?.Invoke();
        Reset();
    }

    public void Reset()
    {
        OnDestroyed?.Invoke(this);
    }
}
