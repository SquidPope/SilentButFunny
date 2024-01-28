using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : GuardState
{
    // State where a guard is following their normal route
    List<Transform> points;
    Transform point; //current target
    int id = 0;
    float speed = 5f;
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
        direction = direction.normalized;
        guard.Direction = direction;
        guard.Rigid.MovePosition(guard.Position + direction * speed * Time.deltaTime);

        //if we're close enough, go to the next point
        if (Vector3.Distance(guard.Position, point.position) <= dist)
        {
            guard.Rigid.MovePosition(point.position);
            id++;
            if (id >= points.Count)
                id = 0;

            point = points[id];
        }
    }
}
