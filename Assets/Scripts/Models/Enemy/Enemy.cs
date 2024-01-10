using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyTriggerZone triggerZone;
    [SerializeField] private Sprite activeEnemySprite;
    
    private float speed;
    private bool isSeePlayer = false;
    private bool isReachedPlayer = false;
    private SpriteRenderer spriteRenderer;

    public static event Action OnReachedPlayer;
    public static event Action OnPlayerInDangeroues;
    public static event Action OnEndOfPlayerDangeroues;

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
    }

    private void OnDisable()
    {
        triggerZone.OnTriggerEnter -= WakeUpEnemy;
    }

    private void WakeUpEnemy()
    {
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
}
