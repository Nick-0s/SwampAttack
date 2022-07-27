using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected int Damage;
    [SerializeField] private float _speed;

    protected Vector2 _direction = Vector2.left;
    
    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            enemy.TakeDamage(Damage);

        Destroy(gameObject);
    }
}
