using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;

    private State _currentState;
    private Player _target;
    public State CurrentState => _currentState;

    private void Start()
    {
        _target = GetComponent<Enemy>().Target;
        
        Reset();
    }

    private void Update()
    {
        if(_currentState == null)
            return;

        var nextState = _currentState.GetNext();

        if (nextState != null)
            Transit(nextState);
    }

    public void Reset()
    {
        _currentState = _firstState;

        if(_currentState != null)
            _currentState.Enter(_target);
    }

    private void Transit(State nextState)
    {
        if(_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if(_currentState != null)
            _currentState.Enter(_target);
    }
}
