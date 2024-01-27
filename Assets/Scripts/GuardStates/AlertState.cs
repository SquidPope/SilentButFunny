using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : GuardState
{
    // State when the guard sees the player
    Vector3 target;
    Vector3 direction;

    float speed = 27f;
    float dist = 0.2f;

    float timer;
    float patience = 7f;

    public AlertState(Guard g)
    {
        guard = g;
    }

    public override void EnterState()
    {
        //ToDo: some sort of sound or visual effect so the player knows they're spotted
        target = PlayerController.Instance.GetPosition();
    }

    public override void Tick()
    {
        if (guard.CanSeePlayer)
        {
            target = PlayerController.Instance.GetPosition();
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= patience)
            {
                guard.ChangeState(); //try to go back to patroling, we lost them.
            }
        }

        direction = target - guard.Position;
        direction = direction.normalized;
        guard.Direction = direction;
        guard.Rigid.MovePosition(guard.Position + direction * speed * Time.deltaTime);

        if (Vector3.Distance(guard.Position, target) < dist)
        {
            //check for player
            guard.ChangeState();
        }
    }
}
