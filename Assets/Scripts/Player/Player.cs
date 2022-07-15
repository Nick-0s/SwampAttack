using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _shootPoint;

    public int Money {get; private set;} = 0;

    public event UnityAction Died;
    private Weapon _currentWeapon;
    private int _currentHealth;
    private Animator _animator;

    private void Start()
    {
        _currentWeapon = _weapons[0];
        _currentHealth = _maxHealth;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _currentWeapon.Shoot(_shootPoint);
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if(_currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Died?.Invoke();

        Destroy(gameObject);
    }

    private void OnEnemyDied(int reward)
    {
        Money += reward;
    }
}