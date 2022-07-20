using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadingIndicator : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Image _indicator;

    private void OnEnable()
    {
        _player.WeaponChanged += OnWeaponChanged;
    }

    private void OnDisable()
    {
        _player.WeaponChanged -= OnWeaponChanged;
        
    }

    private void OnWeaponChanged(Weapon lastWeapon, Weapon newWeapon)
    {
        if(lastWeapon)
            lastWeapon.OnReload -= OnReloading;

        newWeapon.OnReload += OnReloading;
    }

    private void OnReloading(float time)
    {
        StartCoroutine(RenderIndicator(time));
    }

    private IEnumerator RenderIndicator(float time)
    {
        float elapsed = 0;
        _indicator.enabled = true;

        while(elapsed < time)
        {
            _indicator.fillAmount = elapsed / time;
            elapsed += Time.deltaTime;

            yield return null;
        }

        _indicator.enabled = false;
    }
}
