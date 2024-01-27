using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeethProp : Prop
{
    // Prop that attracts nearby guards
    float timer;
    float duration = 15f;

    protected override void GuardEnteredTrigger(GameObject guardObj)
    {
        base.GuardEnteredTrigger(guardObj);

        //attract guard if we're active
        Guard g = guardObj.GetComponent<Guard>(); //ToDo: technically it's in the list, we should be able to get the newest item
        g.DistractionSource = transform.position;
        g.ChangeState(GuardStateType.Distract);

        if (!IsActive)
        {
            IsActive = true;
            timer = 0f;
        }
    }

    protected override void GuardExitedTrigger(GameObject guardObj)
    {
        base.GuardExitedTrigger(guardObj);
    }

    void Update()
    {
        if (IsActive)
        {
            timer += Time.deltaTime;
            if (timer >= duration)
            {
                Debug.Log("timer up");
                IsActive = false;
                foreach(Guard g in nearbyGuards)
                {
                    g.ChangeState();
                }
            }
        }
    }
}
