using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _reward;
    public event UnityAction<int> Died;
    private int _currentHealth;
    private Player _target;

    public Player Target => _target;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void Init(Player target)
    {
        _target = target;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        
        if(_currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Died?.Invoke(_reward);


        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Died -= _target.OnEnemyDied;
    }
}
