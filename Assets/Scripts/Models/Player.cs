using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private float _speed = 10f;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
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
        //transform.Translate(Vector3.zero);
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
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
    }

    private void DestroyPlayer()
    {
        Destroy(gameObject);
    }
}
