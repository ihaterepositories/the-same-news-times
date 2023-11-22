using DG.Tweening;
using UnityEngine;

public class HigherCircleAnimation : MonoBehaviour
{
    private void OnEnable()
    {
        PressKeyToContinueText.OnContinueKeyPressed += IncreaseCircle;
    }

    private void OnDisable()
    {
        PressKeyToContinueText.OnContinueKeyPressed -= IncreaseCircle;
    }

    private void IncreaseCircle()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(new Vector3(50f, 50f, 50f), 1f);
    }
}
