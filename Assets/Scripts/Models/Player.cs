using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Models
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(TrailRenderer))]

    public class Player : MonoBehaviour, IPoolable
    {
        private TrailRenderer _trailRender;
        private readonly float _speed = 10f;

        public static Vector2 Position;

        public GameObject GameObject => gameObject;
        public event Action<IPoolable> OnDestroyed;

        private void Awake()
        {
            _trailRender = GetComponent<TrailRenderer>();
        }

        private void FixedUpdate()
        {
            Move();
            SetPositionVariable();
        }

        private void OnEnable()
        {
            FinishLevelController.OnLevelFinished += ClearTrailRender;
            FinishLevelController.OnLevelFinished += Reset;;
        }

        private void OnDisable()
        {
            FinishLevelController.OnLevelFinished -= ClearTrailRender;
            FinishLevelController.OnLevelFinished -= Reset;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var pickable = collision.gameObject.GetComponent<IPickable>();
            if (pickable is not null) pickable.Pick();
        }

        public void Reset()
        {
            OnDestroyed?.Invoke(this);
        }

        private void Move()
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.up * (_speed * Time.deltaTime));
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.down * (_speed * Time.deltaTime));
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * (_speed * Time.deltaTime));
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * (_speed * Time.deltaTime));
            }
        }

        private void SetPositionVariable()
        {
            Position = transform.position;
        }

        private void ClearTrailRender()
        {
            _trailRender.Clear();
        }
    }
}
