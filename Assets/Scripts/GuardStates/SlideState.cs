using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideState : GuardState
{
    // State where the guard is sliding on a banana peel
    float speed = 30f;
    Vector3 direction;

    public SlideState(Guard g) { guard = g; }

    public override void EnterState()
    {
        //ToDo: Funny sfx, move banana prop with the guard?
        Debug.Log("Entered slide!");
        direction = guard.Direction;
        guard.Rigid.velocity = direction * speed / 2f;
    }

    public override void Tick()
    {
        //ToDo: Limit how long a guard slides?
        guard.Rigid.AddForce(direction * speed * Time.deltaTime);
    }
}
