using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GuardStateType {Alert, Distract, Patrol, Repel, Slide, Stun}
public class Guard : MonoBehaviour
{
    // Enemy the player must stealth around
    //should be a gameover if it hits the player (unless they have a panic item?)
    //follows a route until moved out of it by a prop
    //can be slid, stunned, repelled, distracted, alert(saw the player, heading towards them)
    [SerializeField] List<Transform> patrolRoute;

    Rigidbody2D rigid;

    GuardState currentState;

    PatrolState patrol;
    StunState stun;

    bool canResumePatrol = false;

    public GuardState CurrentState
    {
        get { return currentState; }
        set
        {
            //if our state was patrol and becomes stun, we can just go back to patrol at the end
            if (currentState != null)
                currentState.ExitState();

            if (currentState == patrol && value == stun)
                canResumePatrol = true;

            currentState = value;
            currentState.EnterState();
        }
    }

    public Vector3 Position
    {
        get { return transform.position; }
        set { transform.position = value; } //ToDo: do we need to go through the rigidbody for this?
    }

    public Rigidbody2D Rigid
    {
        get { return rigid; }
    }

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        patrol = new PatrolState(patrolRoute, this);
        stun = new StunState(this);

        CurrentState = patrol; //ToDo: Testing, remove
    }

    public void ChangeState()
    {
        //figure out next state
        //are we on our patrol route (can we see one of the points?) if so, just go to patrol mode and set the visible point as the next one
        //if not, how do we reach a point to see our patrol?

        if (CurrentState != patrol) //If we aren't patroling try to go back to the patrol route
        {
            if (canResumePatrol)
            {
                ChangeState(GuardStateType.Patrol);
                return;
            }
        }
    }

    public void ChangeState(GuardStateType type)
    {
        switch (type)
        {
            case GuardStateType.Alert:
            break;

            case GuardStateType.Distract:
            break;

            case GuardStateType.Patrol:
            CurrentState = patrol;
            break;

            case GuardStateType.Repel:
            break;

            case GuardStateType.Slide:
            break;

            case GuardStateType.Stun:
            CurrentState = stun;
            break;

            default:
            CurrentState = stun;
            Debug.LogError($"Tried to change to invalid type {type}");
            break;
        }
    }

    void Update()
    {
        if (currentState != null)
            currentState.Tick();
    }
}
