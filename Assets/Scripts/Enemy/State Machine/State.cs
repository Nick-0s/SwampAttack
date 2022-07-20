using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;
    protected Player Target { get; private set; }
    protected Animator Animator;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public void Enter(Player target)
    {
        if (enabled == false)
        {
            Target = target;
            enabled = true;

            foreach(var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(Target);
            }
        }
    }

    public State GetNext()
    {
        foreach(var transition in _transitions)
        {
            if(transition.NeedTransit)
                return transition.TargetState;
        }
        
        return null;
    }

    public void Exit()
    {
        if(enabled == true)
        {
            foreach(var transition in _transitions)
                transition.enabled = false;

            enabled = false;
        }
    }
}
