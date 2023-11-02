using UnityEngine;
using DG.Tweening;

public class CircleAnimation : MonoBehaviour
{
    private void OnEnable()
    {
        StartLevelController.OnStartLevel += DecreaseCircle;
        FinishLevelController.OnFinishLevel += IncreaseCircle;
    }

    private void OnDisable()
    {
        StartLevelController.OnStartLevel -= DecreaseCircle;
        FinishLevelController.OnFinishLevel -= IncreaseCircle;
    }

    private void DecreaseCircle()
    {
        transform.localScale = new Vector3(50f, 50f, 50f);
        transform.DOScale(Vector3.zero, 1f);
    }

    private void IncreaseCircle()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(new Vector3(50f, 50f, 50f), 1f);
    }
}
