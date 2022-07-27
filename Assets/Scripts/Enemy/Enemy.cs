using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _minAttackRange;
    [SerializeField] private float _maxAttackRange;
    [SerializeField] private float _rangeSpread;
    [SerializeField] private int _reward;
    public event UnityAction<int> Died;
    private int _currentHealth;
    private Player _target;

    public Player Target => _target;
    public float Speed => _speed;
    public int Damage => _damage;
    public float AttackDelay => _attackDelay;
    public float MinAttackRange => _minAttackRange;
    public float MaxAttackRange => _maxAttackRange;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _minAttackRange += Random.Range(-_rangeSpread, _rangeSpread);
        _maxAttackRange += Random.Range(-_rangeSpread, _rangeSpread);
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

    public virtual void Attack()
    {
        Target.TakeDamage(_damage);
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
