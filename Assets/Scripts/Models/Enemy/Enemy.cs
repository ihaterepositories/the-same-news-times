using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Enemy : MonoBehaviour, IPoolable
{
    [SerializeField] private EnemyTriggerZone triggerZone;
    [SerializeField] private Sprite activeEnemySprite;
    [SerializeField] private ParticleSystem sleepingEffectParticle;

    private float speed;
    private bool isSeePlayer = false;
    private bool isReachedPlayer = false;
    private SpriteRenderer spriteRenderer;

    public GameObject GameObject => gameObject;

    public static event Action OnReachedPlayer;
    public static event Action OnPlayerInDangeroues;
    public static event Action OnEndOfPlayerDangeroues;
    public event Action<IPoolable> OnDestroyed;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (isSeePlayer)
        {
            FollowPlayer();
            CheckPlayerReaching();
            MakeTriggerZoneFading();
        }
    }

    private void OnEnable()
    {
        triggerZone.OnTriggerEnter += WakeUpEnemy;
        FinishLevelController.OnGameFinished += StopParticleEffect;
        FinishLevelController.OnLevelFinished += Reset;
    }

    private void OnDisable()
    {
        triggerZone.OnTriggerEnter -= WakeUpEnemy;
        FinishLevelController.OnGameFinished -= StopParticleEffect;
        FinishLevelController.OnLevelFinished -= Reset;
    }

    public void Reset()
    {
        OnDestroyed?.Invoke(this);
    }

    private void WakeUpEnemy()
    {
        sleepingEffectParticle.Stop();
        MakePlayerVisible();
        ChangeSpriteToActive();
    }

    private void ChangeSpriteToActive()
    {
        spriteRenderer.sprite = activeEnemySprite;
    }

    private void MakePlayerVisible()
    {
        isSeePlayer = true;
    }

    private float GetDistanceToPlayer()
    {
        return Vector2.Distance(transform.position, Player.Position);
    }

    private void FollowPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, Player.Position, speed * Time.deltaTime);
    }

    private void CheckPlayerReaching()
    {
        if (GetDistanceToPlayer() <= 0.4f && !isReachedPlayer)
        {
            OnReachedPlayer?.Invoke();
            isReachedPlayer = true;
        }
    }

    private void MakeTriggerZoneFading()
    {
        if (GetDistanceToPlayer() <= 3f)
        {
            speed = 0.3f;
            triggerZone.SetAlphaOfColor(1f);
            OnPlayerInDangeroues?.Invoke();
        }
        else if (GetDistanceToPlayer() <= 5f)
        {
            speed = 0.2f;
            triggerZone.SetAlphaOfColor(0.75f);
            OnEndOfPlayerDangeroues?.Invoke();
        }
        else if (GetDistanceToPlayer() > 5f)
        {
            speed = 0.1f;
            triggerZone.SetAlphaOfColor(0);
        }
    }

    private void StopParticleEffect()
    {
        sleepingEffectParticle.Stop();
    }
}
