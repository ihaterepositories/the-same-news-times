using System;
using UnityEngine;

public class PinkScore : MonoBehaviour, IPickable, IPoolable
{
    public GameObject GameObject => gameObject;

    public static event Action OnPicked;
    public event Action<IPoolable> OnDestroyed;

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
