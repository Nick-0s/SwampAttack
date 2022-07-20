using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _shootPoint;

    public int Money {get; private set;} = 0;

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<Weapon, Weapon> WeaponChanged;
    public event UnityAction MoneyChanged;
    public event UnityAction Died;
    private Weapon _currentWeapon;
    private int _currentWeaponNumber = 0;
    private int _currentHealth;

    private void Start()
    {
        SetWeapon(_weapons[_currentWeaponNumber]);
        _currentHealth = _maxHealth;
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
        HealthChanged?.Invoke(_currentHealth, _maxHealth);

        if(_currentHealth <= 0)
        {
            _currentHealth = 0;
            HealthChanged?.Invoke(_currentHealth, _maxHealth);

            Die();
        }        
    }

    public void TakeNextWeapon()
    {
        if(_currentWeaponNumber == _weapons.Count - 1)
            _currentWeaponNumber = 0;
        else
            _currentWeaponNumber++;

        SetWeapon(_weapons[_currentWeaponNumber]);
    }

    public void TakePreviousWeapon()
    {
        if(_currentWeaponNumber == 0)
            _currentWeaponNumber = _weapons.Count - 1;
        else
            _currentWeaponNumber--;

        SetWeapon(_weapons[_currentWeaponNumber]);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        MoneyChanged?.Invoke();

        _weapons.Add(weapon);
    }

    public void OnEnemyDied(int reward)
    {
        Money += reward;
        MoneyChanged?.Invoke();
    }

    private void Die()
    {
        Died?.Invoke();

        gameObject.SetActive(false);
    }

    private void SetWeapon(Weapon weapon)
    {
        Weapon lastWeapon = _currentWeapon;

        _currentWeapon = weapon;
        WeaponChanged?.Invoke(lastWeapon, _currentWeapon);
    }
}