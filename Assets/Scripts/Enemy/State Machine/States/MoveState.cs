using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    private string _move = "Move";

    private void OnEnable()
    {
        Animator.Play(_move);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, Enemy.Speed * Time.deltaTime);
    }
}
