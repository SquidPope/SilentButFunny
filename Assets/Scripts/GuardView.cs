using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardView : MonoBehaviour
{
    // Trigger area where the player will be seen by the guard if they enter it
    [SerializeField] Guard guard;

    bool isActive = true;
    bool isPlayerInTrigger = false;

    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;//local?
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsActive)
            return;

        if (other.tag == "Player")
        {
            isPlayerInTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            isPlayerInTrigger = false;
    }

    void Update()
    {
        if (IsActive && isPlayerInTrigger)
        {
            //Checking that the player isn't behind a wall
            RaycastHit2D hit = Physics2D.Linecast(guard.Position, PlayerController.Instance.GetPosition());
            if (hit.collider == null)
                guard.ChangeState(GuardStateType.Alert);
        }
    }
}
