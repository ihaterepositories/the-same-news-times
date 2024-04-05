using System;
using System.Collections;
using Controllers.InGameControllers;
using Models.Enemies.Interfaces;
using Models.Items.Interfaces;
using UnityEngine;

namespace Models
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(TrailRenderer))]

    public class Player : MonoBehaviour
    {
        private TrailRenderer _trailRender;
        private float _speed = 9f;

        public static Vector2 Position;
        
        public static event Action OnDestroyedByEnemy;

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
            Inventory.OnBoosterUsed += Boost;
            LevelFinisher.OnLevelFinished += ClearTrailRender;
        }

        private void OnDisable()
        {
            Inventory.OnBoosterUsed -= Boost;
            LevelFinisher.OnLevelFinished -= ClearTrailRender;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.gameObject.GetComponent<IPickAble>()?.Pick();
            
            var enemy = other.gameObject.GetComponent<IEnemy>();
            if (enemy == null) return;
            enemy.CaughtPlayer();
            OnDestroyedByEnemy?.Invoke();
        }

        private void Boost()
        {
            StartCoroutine(BoostCoroutine());
        }
        
        private IEnumerator BoostCoroutine()
        {
            _speed = 11f;
            yield return new WaitForSeconds(3f);
            _speed = 9f;
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

        public void ClearTrailRender()
        {
            _trailRender.Clear();
        }
        
        public void SetPosition(Vector2 newPosition)
        {
            transform.position = newPosition;
        }
    }
}
