using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class TempleKeeper : MonoBehaviour, IPoolable
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TempleKeeperTriggerZone triggerZone;
    [SerializeField] private Sprite activeEnemySprite;
    [SerializeField] private Sprite sleepingEnemySprite;
    [SerializeField] private ParticleSystem sleepingEffectParticle;

    private float speed;
    private bool isSeePlayer;
    private bool isReachedPlayer;

    public GameObject GameObject => gameObject;

    public static event Action OnCatchedPlayer;
    public static event Action OnPlayerInDangeroues;
    public static event Action OnEndOfPlayerDangeroues;
    public event Action<IPoolable> OnDestroyed;

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
        FinishLevelController.OnLevelFinished += Reset;
    }

    private void OnDisable()
    {
        triggerZone.OnTriggerEnter -= WakeUpEnemy;
        FinishLevelController.OnLevelFinished -= Reset;
    }

    public void Reset()
    {
        OnDestroyed?.Invoke(this);
    }

    private void WakeUpEnemy()
    {
        isSeePlayer = true;
        sleepingEffectParticle.Stop();
        spriteRenderer.sprite = activeEnemySprite;
    }

    public void MakeEnemySleep()
    {
        isSeePlayer = false;
        isReachedPlayer = false;
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
        transform.position = Vector3.Lerp(transform.position, Player.Position, speed * Time.deltaTime);
    }

    private void CheckPlayerReaching()
    {
        if (GetDistanceToPlayer() <= 0.4f && !isReachedPlayer)
        {
            OnCatchedPlayer?.Invoke();
            isReachedPlayer = true;
        }
    }

    private void MakeTriggerZoneFading()
    {
        if (GetDistanceToPlayer() <= 3f)
        {
            speed = 0.3f;
            triggerZone.SetAlphaOfColor(1f);
            triggerZone.transform.localScale = new Vector2(80f, 80f);
            OnPlayerInDangeroues?.Invoke();
        }
        else if (GetDistanceToPlayer() <= 5f)
        {
            speed = 0.2f;
            triggerZone.SetAlphaOfColor(0.75f);
            triggerZone.transform.localScale = new Vector2(80f, 80f);
            OnEndOfPlayerDangeroues?.Invoke();
        }
        else if (GetDistanceToPlayer() > 5f)
        {
            speed = 0.1f;
            triggerZone.transform.localScale = new Vector2(80f, 80f);
            triggerZone.SetAlphaOfColor(0);
        }
    }
}
