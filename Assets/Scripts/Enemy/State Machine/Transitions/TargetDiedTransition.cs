using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDiedTransition : Transition
{
    private void OnDisable()
    {
        Target.Died -= RequestTransit;        
    }

    public override void Init(Player target)
    {
        base.Init(target);
        Target.Died += RequestTransit;
    }

    private void RequestTransit()
    {
        NeedTransit = true;
    }
}
