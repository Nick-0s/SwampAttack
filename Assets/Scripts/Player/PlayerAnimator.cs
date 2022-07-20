using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]
public class PlayerAnimator : MonoBehaviour
{
    private Player _player;
    private Animator _animator;
    private string _idle = "Idle";
    private string _shoot = "Shoot";

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
        _player.WeaponChanged += OnWeaponChanged;
    }

    private void Start()
    {
        _animator.Play(_idle);
    }

    private void OnDisable()
    {
        _player.WeaponChanged -= OnWeaponChanged;        
    }

    private void OnWeaponChanged(Weapon lastWeapon, Weapon newWeapon)
    {
        if(lastWeapon)
            lastWeapon.Shooted -= OnShoot;

        newWeapon.Shooted += OnShoot;
    }

    private void OnShoot()
    {
        _animator.Play(_shoot);
    }
}
