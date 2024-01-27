using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepelState : GuardState
{
    // State where the guard is repeled by a prop- basically a reverse
    float speed = 22f;
    Vector3 direction;

    public RepelState(Guard g) { guard = g; }

    public override void EnterState()
    {
        direction = -guard.Direction;
    }

    public override void Tick()
    {
        guard.Rigid.MovePosition(guard.Position + direction * speed * Time.deltaTime);
    }
}
