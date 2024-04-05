using DG.Tweening;
using UnityEngine;

namespace Animations
{
    public class IncreaseByKeyPress : MonoBehaviour
    {
        [SerializeField] private KeyCode keyCode;
        private Vector3 _standardScale;

        private void Awake()
        {
            _standardScale = transform.localScale;
        }

        private void Update()
        {
            if (Input.GetKey(keyCode))
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
            transform.DOScale(_standardScale + new Vector3(0.5f, 0.5f, 0.5f), 0.5f);
        }

        private void DoDecrease()
        {
            if (transform.localScale != _standardScale)
                transform.DOScale(_standardScale, 0.5f);
        }
    }
}
