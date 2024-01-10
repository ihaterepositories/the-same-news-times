using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private float speed = 10f;

    public static Vector2 Position;

    private void FixedUpdate()
    {
        Move();
        SetPositionVariable();
    }

    private void OnEnable()
    {
        Timer.OnTimerFinish += DestroyPlayer;
    }

    private void OnDisable()
    {
        Timer.OnTimerFinish -= DestroyPlayer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var eateable = collision.gameObject.GetComponent<IEatable>();

        if (eateable is not null)
        {
            eateable.Eated();
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    private void DestroyPlayer()
    {
        Destroy(gameObject);
    }

    private void SetPositionVariable()
    {
        Position = transform.position;
    }
}
