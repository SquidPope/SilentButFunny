using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : GuardState
{
    // State where a guard is following their normal route
    List<Transform> points;
    Transform point; //current target
    int id = 0;
    float speed = 18f;
    float dist = 0.25f; //distance at which we should just set our position to the current point and move to the next one.

    public PatrolState (List<Transform> patrolPoints, Guard g)
    {
        points = patrolPoints;
        guard = g;
    }

    //Use this to start at a specific point
    public void SetPoint(Transform nextPoint)
    {
        point = nextPoint;
    }

    public override void EnterState()
    {
        if (point == null)
            point = points[0];
    }

    public override void Tick()
    {
        //move to current point
        Vector3 direction = point.position - guard.Position;
        direction = direction.normalized * speed * Time.deltaTime;
        guard.Rigid.MovePosition(guard.Position + direction);

        //if we're close enough, go to the next point
        if (Vector3.Distance(guard.Position, point.position) <= dist)
        {
            guard.Position = point.position;
            id++;
            if (id >= points.Count)
                id = 0;

            point = points[id];
        }
    }

    public override void EndState()
    {
        point = null;
    }
}
