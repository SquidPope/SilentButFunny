using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : GuardState
{
    // State when the guard has been stunned
    //ToDo: add a swirling effect around their head- stars or birds
    //ToDo: sfx

    float duration = 5f; //ToDo: variable duration based on source of stun?
    float timer;

    public StunState(Guard g)
    {
        guard = g;
    }

    public override void EnterState()
    {
        timer = 0f;
    }

    public override void Tick()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            guard.ChangeState();
        }
    }
}
