using System;
using System.Collections;
using Controllers;
using DG.Tweening;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Models.Enemies
{
    public class Ghost : MonoBehaviour, IPoolAble
    {
        [SerializeField] private SpriteRenderer spriteRender;
        
        private float _angularSpeed = 1f;
        private float _circleRadius = 1f;
        private Vector2 _fixedPoint;
        private float _currentAngle;

        private Coroutine _transparencyAnimationCoroutine;

        public GameObject GameObject => gameObject;

        public event Action<IPoolAble> OnDestroyed;
        public static event Action OnCaughtPlayer;

        private void Update()
        {
            DoCircleMoving();
        }

        private void OnEnable()
        {
            FinishLevelController.OnLevelFinished += Reset;
            FinishLevelController.OnGameFinished += Reset;
        }

        private void OnDisable()
        {
            FinishLevelController.OnLevelFinished -= Reset;
            FinishLevelController.OnGameFinished -= Reset;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var player = collision.gameObject.GetComponent<Player>();
            if (player != null) OnCaughtPlayer?.Invoke();
        }

        public void Reset()
        {
            StopHunting();
            OnDestroyed?.Invoke(this);
        }
        
        private void DoCircleMoving()
        {
            _currentAngle += _angularSpeed * Time.deltaTime;
            Vector2 offset = new Vector2(Mathf.Sin(_currentAngle), Mathf.Cos(_currentAngle)) * _circleRadius;
            transform.position = _fixedPoint + offset;
        }

        private void StopHunting()
        {
            if (_transparencyAnimationCoroutine == null) return;
            StopCoroutine(_transparencyAnimationCoroutine);
            _transparencyAnimationCoroutine = null;
        }

        public void StartHunting()
        {
            _transparencyAnimationCoroutine = StartCoroutine(TransparencyAnimationCoroutine());
            GenerateCircleMovingParameters();
            _fixedPoint = transform.position;
        }

        // ReSharper disable once FunctionRecursiveOnAllPaths
        private IEnumerator TransparencyAnimationCoroutine()
        {
            yield return new WaitForSeconds(1);
            spriteRender.DOFade(0.3f, 1f);
            yield return new WaitForSeconds(1);
            spriteRender.DOFade(1f, 1f);
            StartCoroutine(TransparencyAnimationCoroutine());
        }

        private void GenerateCircleMovingParameters()
        {
            _angularSpeed = Random.Range(0.7f, 1.3f);
            _circleRadius = Random.Range(2f, 4f);
        }
    }
}
