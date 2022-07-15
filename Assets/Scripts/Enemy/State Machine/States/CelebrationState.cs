using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelebrationState : State
{
    private string _celebration = "Celebration";

    private void OnEnable()
    {
        Animator.Play(_celebration);
    }

    private void OnDisable()
    {
        Animator.StopPlayback();
    }
}
