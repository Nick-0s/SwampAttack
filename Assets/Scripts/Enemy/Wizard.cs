using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Enemy
{
    [SerializeField] private Transform _castPoint;
    [SerializeField] private float _spellCastDelay;
    [SerializeField] private WizardSpell _spell;

    public override void Attack()
    {
        Debug.Log("Attack");
        StartCoroutine(InstantiateSpellWithDelay(_spellCastDelay));
    }

    private IEnumerator InstantiateSpellWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Instantiate(_spell, _castPoint.position, Quaternion.identity);
    }
}
