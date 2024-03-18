using System;
using Controllers;
using Controllers.InGameControllers;
using Interfaces;
using UnityEngine;

namespace Models
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(TrailRenderer))]

    public class Player : MonoBehaviour, IPoolAble
    {
        private TrailRenderer _trailRender;
        private const float Speed = 10f;

        public static Vector2 Position;

        public GameObject GameObject => gameObject;
        public event Action<IPoolAble> OnDestroyed;

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
            LevelFinisher.OnLevelFinished += ClearTrailRender;
            LevelFinisher.OnLevelFinished += Reset;
            LevelFinisher.OnGameFinished += Reset;
        }

        private void OnDisable()
        {
            LevelFinisher.OnLevelFinished -= ClearTrailRender;
            LevelFinisher.OnLevelFinished -= Reset;
            LevelFinisher.OnGameFinished -= Reset;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var pickAble = collision.gameObject.GetComponent<IPickAble>();
            pickAble?.Pick();
        }

        public void Reset()
        {
            OnDestroyed?.Invoke(this);
        }

        private void Move()
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.up * (Speed * Time.deltaTime));
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.down * (Speed * Time.deltaTime));
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * (Speed * Time.deltaTime));
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * (Speed * Time.deltaTime));
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
