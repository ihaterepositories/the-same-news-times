using System;
using UnityEngine;

public class Lock : MonoBehaviour, IPoolable
{
    public GameObject GameObject => gameObject;

    public event Action<IPoolable> OnDestroyed;

    private void OnEnable()
    {
        Key.OnPicked += Reset;
    }

    private void OnDisable()
    {
        Key.OnPicked -= Reset;
    }

    public void Reset()
    {
        OnDestroyed?.Invoke(this);
    }
}
