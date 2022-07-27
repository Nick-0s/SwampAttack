using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardSpell : Bullet
{
    private void OnEnable()
    {
        _direction = Vector2.right;   
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
            player.TakeDamage(Damage);

        Destroy(gameObject);
    }
}
