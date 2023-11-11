using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GreenPoint : MonoBehaviour, IEatable
{
    public static event Action OnEated;

    private void Start()
    {
        
    }

    public void Eated()
    {
        OnEated?.Invoke();
        Destroy(gameObject);
    }
}
