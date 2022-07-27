using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private float _cooldown;
    private string _attack = "Attack";

    private void Update()
    {
        if (_cooldown <= 0)
        {
            PlayAttack();
            Enemy.Attack();
            _cooldown = Enemy.AttackDelay;
        }
        
        _cooldown -= Time.deltaTime;
    }

    private void PlayAttack()
    {
        Animator.Play(_attack);
    }
}
