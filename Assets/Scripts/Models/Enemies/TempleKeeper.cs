using System;
using Controllers.InGameControllers;
using Interfaces;
using UnityEngine;

namespace Models.Enemies
{
    [RequireComponent(typeof(SpriteRenderer))]

    public class TempleKeeper : MonoBehaviour, IPoolAble, IEnemy
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private TempleKeeperTriggerZone triggerZone;
        [SerializeField] private Sprite activeEnemySprite;
        [SerializeField] private Sprite sleepingEnemySprite;
        [SerializeField] private ParticleSystem sleepingEffectParticle;

        private float _speed;
        private bool _isSeePlayer;

        public GameObject GameObject => gameObject;

        public static event Action OnPlayerInDangerous;
        public static event Action OnEndOfPlayerDangerous;
        public event Action<IPoolAble> OnDestroyed;

        private void FixedUpdate()
        {
            if (!_isSeePlayer) return;
            FollowPlayer();
            MakeTriggerZoneFading();
        }

        private void OnEnable()
        {
            triggerZone.OnTriggerEnter += WakeUpEnemy;
            LevelFinisher.OnLevelFinished += Reset;
            LevelFinisher.OnGameFinished += Reset;
        }

        private void OnDisable()
        {
            triggerZone.OnTriggerEnter -= WakeUpEnemy;
            LevelFinisher.OnLevelFinished -= Reset;
            LevelFinisher.OnGameFinished -= Reset;
        }

        public void CaughtPlayer()
        {
            Reset();
        }
        
        public void Reset()
        {
            OnDestroyed?.Invoke(this);
        }

        private void WakeUpEnemy()
        {
            _isSeePlayer = true;
            sleepingEffectParticle.Stop();
            spriteRenderer.sprite = activeEnemySprite;
        }

        public void MakeEnemySleep()
        {
            _isSeePlayer = false;
            sleepingEffectParticle.Play();
            triggerZone.SetAlphaOfColor(0.15f);
            spriteRenderer.sprite = sleepingEnemySprite;
            triggerZone.StartBreathing();
        }

        private float GetDistanceToPlayer()
        {
            return Vector2.Distance(transform.position, Player.Position);
        }

        private void FollowPlayer()
        {
            transform.position = Vector3.Lerp(transform.position, Player.Position, _speed * Time.deltaTime);
        }

        private void MakeTriggerZoneFading()
        {
            if (GetDistanceToPlayer() <= 3f)
            {
                _speed = 0.15f;
                triggerZone.SetAlphaOfColor(1f);
                triggerZone.transform.localScale = new Vector2(80f, 80f);
                OnPlayerInDangerous?.Invoke();
            }
            else if (GetDistanceToPlayer() <= 5f)
            {
                _speed = 0.2f;
                triggerZone.SetAlphaOfColor(0.75f);
                triggerZone.transform.localScale = new Vector2(80f, 80f);
                OnEndOfPlayerDangerous?.Invoke();
            }
            else if (GetDistanceToPlayer() > 5f)
            {
                _speed = 0.25f;
                triggerZone.transform.localScale = new Vector2(80f, 80f);
                triggerZone.SetAlphaOfColor(0);
            }
        }
    }
}
