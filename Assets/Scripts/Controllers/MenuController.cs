using System;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static event Action OnMenuEntered;

    private void Start()
    {
        OnMenuEntered?.Invoke();

        CircleAnimation.Instance.Decrease();
    }
}
