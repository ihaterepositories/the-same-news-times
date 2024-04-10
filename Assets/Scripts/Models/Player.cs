using System;
using System.Collections;
using Controllers.InGameControllers;
using DG.Tweening;
using Models.Enemies.Interfaces;
using Models.Items.Interfaces;
using UnityEngine;

namespace Models
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(TrailRenderer))]

    public class Player : MonoBehaviour
    {
        [SerializeField] private Gradient defaultTrailGradient;
        [SerializeField] private Gradient poisonedTrailGradient;
        [SerializeField] private Gradient boostedTrailGradient;
        
        private TrailRenderer _trailRender;

        private const float DefaultSpeed = 9f;
        private float _speed;

        public bool IsBoosted { get; private set; }
        public static Vector2 Position;

        public static event Action OnPoisoned;
        public static event Action OnDePoisoned;
        public static event Action OnDestroyedByEnemy;

        private void Awake()
        {
            _trailRender = GetComponent<TrailRenderer>();
            _speed = DefaultSpeed;
        }

        private void FixedUpdate()
        {
            Move();
            SetPositionVariable();
        }

        private void OnEnable()
        {
            LevelFinisher.OnLevelFinished += ClearTrailRender;
        }

        private void OnDisable()
        {
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
        
        public IEnumerator BoostCoroutine()
        {
            IsBoosted = true;
            ChangeTrailGradient(boostedTrailGradient, 1f);
            _speed += 4f;
            yield return new WaitForSeconds(8f);
            _speed = DefaultSpeed;
            ChangeTrailGradient(defaultTrailGradient, 1f);
            IsBoosted = false;
        }
        
        public IEnumerator SlowDownCoroutine()
        {
            OnPoisoned?.Invoke();
            ChangeTrailGradient(poisonedTrailGradient, 1f);
            _speed -= 4f;
            yield return new WaitForSeconds(8f);
            _speed = DefaultSpeed;
            ChangeTrailGradient(defaultTrailGradient, 1f);
            OnDePoisoned?.Invoke();
        }
        
        private void ChangeTrailGradient(Gradient newGradient, float duration)
        {
            for (var i = 0; i < _trailRender.colorGradient.colorKeys.Length; i++)
            {
                var index = i;
                DOTween.To(() => _trailRender.colorGradient.colorKeys[index].color, 
                    x => SetColorKey(index, x), 
                    newGradient.colorKeys[index].color, 
                    duration);
            }
        }

        private void SetColorKey(int index, Color newColor)
        {
            var colorGradient = _trailRender.colorGradient;
            var colorKeys = colorGradient.colorKeys;
            colorKeys[index].color = newColor;
            var newGradient = new Gradient { colorKeys = colorKeys, alphaKeys = colorGradient.alphaKeys };
            colorGradient = newGradient;
            _trailRender.colorGradient = colorGradient;
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
