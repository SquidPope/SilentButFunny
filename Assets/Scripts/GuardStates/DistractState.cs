using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class DistractState : GuardState
{
    // State where the guard is distracted by a prop- should move towards it and stay there until it deactivates
    float speed = 18f;
    float dist = 0.2f;

    Vector3 distractionSource;
    bool isMoving;

    public DistractState(Guard g) { guard = g; }

    public override void EnterState()
    {
        distractionSource = guard.DistractionSource;
        isMoving = true;
    }

    public override void Tick()
    {
        if (isMoving)
        {
            Vector3 direction = distractionSource - guard.Position; //we need the prop's position here
            direction = direction.normalized;
            guard.Direction = direction;
            guard.Rigid.MovePosition(guard.Position + direction * speed * Time.deltaTime);

            if (Vector3.Distance(distractionSource, guard.Position) < dist)
            {
                isMoving = false;
            }
        }
        //Guard is enthralled by the distraction and won't leave until it deactivates- unless the player enters their view area.
    }
}
