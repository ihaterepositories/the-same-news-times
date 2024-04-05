using DG.Tweening;
using UnityEngine;

namespace AnimationControllers
{
    public class DecreaseByKeyPress : MonoBehaviour
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
                DoDecrease();
            }
            else
            {
                DoIncrease();
            }
        }

        private void DoDecrease()
        {
            transform.DOScale(_standardScale / 2, 0.5f);
        }

        private void DoIncrease()
        {
            if (transform.localScale != _standardScale)
                transform.DOScale(_standardScale, 0.5f);
        }
    }
}
