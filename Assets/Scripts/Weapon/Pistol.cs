using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    public override void Shoot(Transform shootPoint)
    {
        if(IsReadyToShoot)
        {
            IsReadyToShoot = false;
            ShotsDone++;
            InvokeShooted();
            Instantiate(Bullet, shootPoint.position, Quaternion.identity);

            Coroutine delay = StartCoroutine(ShootingDelay());

            if(ShotsDone == ShotsBeforeReloading)
            {
                if(delay != null)
                    StopCoroutine(delay);

                Reload();
            }
        }
    }
}
