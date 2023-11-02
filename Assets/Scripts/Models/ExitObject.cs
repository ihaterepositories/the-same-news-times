using System;
using UnityEngine;

public class ExitObject : MonoBehaviour, IEatable
{
    public static event Action OnEated;

    

    public void Eated()
    {
        OnEated?.Invoke();
        Destroy(gameObject);
    }
}
