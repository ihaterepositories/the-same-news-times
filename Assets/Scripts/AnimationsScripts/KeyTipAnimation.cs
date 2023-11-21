using DG.Tweening;
using UnityEngine;

public class KeyTipAnimation : MonoBehaviour
{
    [SerializeField] private KeyCode _keyCode;

    private float scaleX;
    private float scaleY;

    private void Awake()
    {
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
    }

    private void Update()
    {
        ChangeScale();
    }

    private void ChangeScale()
    {
        if (Input.GetKey(_keyCode))
        {
            transform.DOScaleX(scaleX / 2, 0.5f);
            transform.DOScaleY(scaleY / 2, 0.5f);
        }
        else
        {
            transform.DOScaleX(scaleX, 0.5f);
            transform.DOScaleY(scaleY, 0.5f);
        }
    }
}
