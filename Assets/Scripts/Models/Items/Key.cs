using System;
using UnityEngine;

public class Key : MonoBehaviour, IPickable, IPoolable
{
    public GameObject GameObject => gameObject;

    public event Action<IPoolable> OnDestroyed;
    public static event Action OnPicked;

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
