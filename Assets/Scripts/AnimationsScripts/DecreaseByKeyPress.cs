using DG.Tweening;
using UnityEngine;

public class DecreaseByKeyPress : MonoBehaviour
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
            DoDecrease();
        }
        else
        {
            DoIncrease();
        }
    }

    private void DoDecrease()
    {
        transform.DOScale(_standartScale / 2, 0.5f);
    }

    private void DoIncrease()
    {
        if (transform.localScale != _standartScale)
            transform.DOScale(_standartScale, 0.5f);
    }
}
