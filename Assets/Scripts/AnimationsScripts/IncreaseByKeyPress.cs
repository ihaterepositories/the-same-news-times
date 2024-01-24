using DG.Tweening;
using UnityEngine;

public class IncreaseByKeyPress : MonoBehaviour
{
    [SerializeField] private KeyCode _keyCode;
    private Vector3 _standartScale;

    private void Awake()
    {
        _standartScale = transform.localScale;
    }

    private void Update()
    {
        if (Input.GetKey(_keyCode))
        {
            DoIncrease();
        }
        else
        {
            DoDecrease();
        }
    }

    private void DoIncrease()
    {
        transform.DOScale(_standartScale + new Vector3(0.5f, 0.5f, 0.5f), 0.5f);
    }

    private void DoDecrease()
    {
        if (transform.localScale != _standartScale)
        transform.DOScale(_standartScale, 0.5f);
    }
}
