using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GuardStateType {Alert, Distract,FollowPath, Patrol, Repel, Slide, Stun}
public class Guard : MonoBehaviour
{
    // Enemy the player must stealth around
    //should be a gameover if it hits the player (unless they have a panic item?)
    //follows a route until moved out of it by a prop, or spotting the player
    //can be slid, stunned, repelled, distracted, alert(saw the player, heading towards them)

    [SerializeField] GuardView view;
    [SerializeField] List<Transform> patrolRoute;
    [SerializeField] GameObject alertObj;

    List<Vector3> path; //List of positions we can follow back to our patrol route.

    Rigidbody2D rigid;

    GuardState currentState;

    AlertState alert;
    DistractState distract;
    FollowPathState followPath;
    PatrolState patrol;
    RepelState repel;
    SlideState slide;
    StunState stun;

    bool canResumePatrol = false;
    bool canSeePlayer = false;

    Vector3 direction;
    Vector3 distractionSource;

    float pathTimer = 0f;
    float pathInterval = 1f;

    public Vector3 Direction
    {
        get { return direction; }
        set
        {
            direction = value;
            view.SetPosition(transform.position + direction);
            view.Facing = direction;
        }
    }

    public bool CanSeePlayer
    {
        get { return canSeePlayer; }
        set { canSeePlayer = value; }
    }

    public GuardState CurrentState
    {
        get { return currentState; }
        set
        {
            //if our state was patrol and becomes stun, we can just go back to patrol at the end
            if (CurrentState == value) //Don't set the state again if we're already in that state.
                return;

            if (currentState != null)
                currentState.ExitState();

            if (currentState == patrol && value == stun)
                canResumePatrol = true;

            currentState = value;

            if (currentState == slide || currentState == stun) //Don't catch the player while stunned or sliding
                view.IsActive = false;
            else
                view.IsActive = true;

            if (currentState == alert)
                alertObj.SetActive(true);
            else
                alertObj.SetActive(false);
            
            currentState.EnterState();
        }
    }

    public Vector3 Position
    {
        get { return transform.position; }
        set { transform.position = value; } //ToDo: do we need to go through the rigidbody for this?
    }

    public Vector3 DistractionSource
    {
        get { return distractionSource; }
        set { distractionSource = value; }
    }

    public Rigidbody2D Rigid
    {
        get { return rigid; }
    }

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();

        alert = new AlertState(this);
        distract = new DistractState(this);
        followPath = new FollowPathState(this);
        patrol = new PatrolState(patrolRoute, this);
        repel = new RepelState(this);
        slide = new SlideState(this);
        stun = new StunState(this);

        CurrentState = patrol; //ToDo: Testing, remove

        path = new List<Vector3>();
        alertObj.SetActive(false);

        view.IsActive = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (CurrentState == slide)
        {
            //we hit something in a slide
            CurrentState = stun;
            return;
        }
        else if (CurrentState == repel)
        {
            ChangeState();
        }
        else if (CurrentState == alert)
        {
            //we hit a wall trying to reach the player's location
            //we should either give up, or shimmy left or right to try and get around
        }

        if (other.gameObject.tag == "Player")
        {
            GameController.Instance.State = GameState.Over;
        }
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
            else
            {
                //try to see if we can reach one of the points
                for (int i = 0; i < patrolRoute.Count; i++)
                {
                    //linecast towards the point
                    RaycastHit2D hit;
                    hit = Physics2D.Linecast(transform.position, patrolRoute[i].position);

                    if (hit.collider == null)
                    {
                        //we found a point!
                        patrol.SetPoint(patrolRoute[i]);
                        CurrentState = patrol;
                        return;
                    }
                    else
                    {
                        //There's something in the way, try another node.
                        Debug.Log($"Hit {hit.collider.gameObject.name}!");
                    }
                }

                //If we reach here it means we can't see any of the patrol points, follow the path we made back
                if (path.Count > 0)
                {
                    followPath.SetPath(path);
                    ChangeState(GuardStateType.FollowPath);
                }
            }
        }
        //Come to think of it, if we are patroling we shouldn't be here...
    }

    public void ChangeState(GuardStateType type)
    {
        Debug.Log($"{gameObject.name} changing to state {type}");

        switch (type)
        {
            case GuardStateType.Alert:
            CurrentState = alert;
            AudioManager.Instance.PlaySFX(SFXType.Alert);
            break;

            case GuardStateType.Distract:
            CurrentState = distract;
            break;

            case GuardStateType.FollowPath:
            CurrentState = followPath;
            break;

            case GuardStateType.Patrol:
            CurrentState = patrol;
            path.Clear(); //We don't need the path if we're back to patroling.
            break;

            case GuardStateType.Repel:
            CurrentState = repel;
            break;

            case GuardStateType.Slide:
            CurrentState = slide;
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

    void FixedUpdate()
    {
        if (currentState != null)
            currentState.Tick();

        if (CurrentState == slide || CurrentState == repel || CurrentState == alert)
        {
            pathTimer += Time.deltaTime;
            if (pathTimer >= pathInterval)
            {
                pathTimer = 0f;
                path.Add(transform.position);
            }
        }
    }
}
