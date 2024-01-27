using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardView : MonoBehaviour
{
    // Trigger area where the player will be seen by the guard if they enter it
    [SerializeField] Guard guard;

    bool isActive = true;
    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;//local?
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsActive)
            return;

        if (other.tag == "Player")
        {
            guard.ChangeState(GuardStateType.Alert);
        }
    }
}
