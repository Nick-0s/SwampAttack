using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delay;

    private float _cooldown;
    private string _attack = "Attack";

    private void Update()
    {
        if (_cooldown <= 0)
        {
            Attack();

            _cooldown = _delay;
        }
        
        _cooldown -= Time.deltaTime;
    }

    private void Attack()
    {
        Animator.Play(_attack);
        Target.TakeDamage(_damage);
    }
}
