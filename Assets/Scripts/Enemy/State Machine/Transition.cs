using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    protected Enemy Enemy;

    protected Player Target {get; private set;}
    public bool NeedTransit { get; protected set;}
    public State TargetState => _targetState;

    private void OnEnable()
    {
        NeedTransit = false;
    }

    private void Awake()
    {
        Enemy = GetComponent<Enemy>();
    }

    public virtual void Init(Player target)
    {
        Target = target;
    }
}
