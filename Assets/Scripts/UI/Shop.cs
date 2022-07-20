using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Player _player;
    [SerializeField] private WeaponView _template;
    [SerializeField] private GameObject _container;

    private void Start()
    {
        foreach(Weapon weapon in _weapons)
            AddItem(weapon);
    }

    private void OnBuyButtonClick(Weapon weapon, WeaponView view)
    {
        TrySellWeapon(weapon, view);
    }

    private void TrySellWeapon(Weapon weapon, WeaponView view)
    {
        if(_player.Money >= weapon.Price)
        {
            _player.BuyWeapon(weapon);
            weapon.Buy();
            view.BuyButtonClicked -= OnBuyButtonClick;
        }
    }

    private void AddItem(Weapon weapon)
    {
        var view = Instantiate(_template, _container.transform);
        view.Render(weapon);
        view.BuyButtonClicked += OnBuyButtonClick;
    }
}
