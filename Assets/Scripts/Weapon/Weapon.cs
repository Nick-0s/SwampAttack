using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private string _lable;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isBought = false;
    [SerializeField] private float _reloadingTime;
    [SerializeField] private float _delayBetweenShots;
    [SerializeField] protected int ShotsBeforeReloading;
    [SerializeField] protected Bullet Bullet;
    [SerializeField] protected bool IsReadyToShoot;

    public event UnityAction<float> OnReload;
    public event UnityAction Shooted;
    protected int ShotsDone;

    public string Lable => _lable;
    public int Price => _price;
    public Sprite Icon => _icon;
    public bool IsBought => _isBought;

    public abstract void Shoot(Transform shootPoint);

    public void Buy()
    {
        _isBought = true;
    }

    protected void InvokeShooted()
    {
        Shooted?.Invoke();
    }

    protected void Reload()
    {
        IsReadyToShoot = false;
        ShotsDone = 0;
        OnReload?.Invoke(_reloadingTime);

        StartCoroutine(Reloading());
    }

    protected IEnumerator ShootingDelay()
    {
        yield return new WaitForSeconds(_delayBetweenShots);

        IsReadyToShoot = true;
    }

    private IEnumerator Reloading()
    {
        yield return new WaitForSeconds(_reloadingTime);

        IsReadyToShoot = true;
    }
}
