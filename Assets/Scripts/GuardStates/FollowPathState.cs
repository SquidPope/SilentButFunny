using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FollowPathState : GuardState
{
    // State that follows a list of points until it reaches the end
    List<Vector3> path;
    Vector3 target;
    Vector3 direction;

    float speed = 5f;
    float dist = 0.1f;

    public FollowPathState(Guard g) { guard = g; }

    public void SetPath(List<Vector3> path)
    {
        this.path = path;
    }

    public override void EnterState()
    {
        target = path[path.Count - 1];
    }

    public override void Tick()
    {
        //move towards latest point
        direction = target - guard.Position;
        direction = direction.normalized;
        guard.Direction = direction;
        guard.Rigid.MovePosition(direction * speed * Time.deltaTime);

        //when we're close enough, remove that point from the list and get the next one
        if (Vector3.Distance(target, guard.Position) < dist)
        {
            path.Remove(target);
            if (path.Count > 0)
            {
                target = path[path.Count - 1];
            }
            else
            {
                //when we run out of points, tell the guard to change state
                guard.ChangeState();
            }
        }
    }
}
