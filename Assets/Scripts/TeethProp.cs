using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeethProp : Prop
{
    // Prop that attracts nearby guards
    float timer = 0f;
    float duration = 15f;

    protected override void GuardEnteredTrigger(GameObject guardObj)
    {
        base.GuardEnteredTrigger(guardObj);

        //attract guard if we're active

        Guard g = nearbyGuards.Find(x => x.gameObject == guardObj); // should be the newest item
        g.DistractionSource = transform.position;
        g.ChangeState(GuardStateType.Distract);

        AudioManager.Instance.PlaySFX(SFXType.TeethChatter);
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
                timer = 0f;

                foreach (Guard g in nearbyGuards)
                {
                    g.ChangeState();
                }
            }
        }
    }
}
