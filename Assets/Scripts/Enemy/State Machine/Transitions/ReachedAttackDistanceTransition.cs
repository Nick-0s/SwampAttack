using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachedAttackDistanceTransition : Transition
{
    private void Update()
    {
        float distanse = Vector2.Distance(transform.position, Target.transform.position);

        if (distanse <= Enemy.MaxAttackRange && distanse >= Enemy.MinAttackRange)
            NeedTransit = true;
    }
}
