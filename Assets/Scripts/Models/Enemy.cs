using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 3f;
    private Vector2 _direction;

    private void Start()
    {
        StartCoroutine(ChangeDirectionCoroutine());
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private IEnumerator ChangeDirectionCoroutine()
    {
        int directionType = UnityEngine.Random.Range(1, 5);

        if (directionType == 1)
        {
            _direction = new Vector2(1, 0);
        }
        else if (directionType == 2)
        {
            _direction = new Vector2(-1, 0);
        }
        else if (directionType == 3)
        {
            _direction = new Vector2(0, 1);
        }
        else if (directionType == 4)
        {
            _direction = new Vector2(0, -1);
        }

        yield return new WaitForSeconds(Random.Range(1f, 2f));
        StartCoroutine(ChangeDirectionCoroutine());
    }
}
