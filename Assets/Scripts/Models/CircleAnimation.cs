using UnityEngine;
using DG.Tweening;

public class CircleAnimation : MonoBehaviour
{
    private void Start()
    {
        DecreaseCircle();
    }

    private void DecreaseCircle()
    {
        transform.DOScale(Vector3.zero, 1f);
    }

    private void IncreaseCircle()
    {
        transform.DOScale(new Vector3(50f, 50f, 50f), 1f);
    }
}
