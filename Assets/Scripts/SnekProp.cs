using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekProp : Prop
{
    // Prop that stuns nearby guards after a delay
    //ToDo: Countdown displaying over the object would be nice

    bool armed = false;
    float timer = 0f;
    float timerMax = 3f; //ToDo: this feels right, make sure guards aren't too fast for it though

    protected override void GuardEnteredTrigger(GameObject guardObj)
    {
        if (!IsUsed)
        {
            base.GuardEnteredTrigger(guardObj);
            armed = true;

            Debug.Log($"There are {nearbyGuards.Count} guards here");
        }
    }

    protected override void GuardExitedTrigger(GameObject guardObj)
    {
        base.GuardExitedTrigger(guardObj);
    }

    void Update()
    {
        if (armed)
        {
            timer += Time.deltaTime;
            if (timer >= timerMax)
            {
                //BOOM
                foreach (Guard g in nearbyGuards)
                {
                    g.ChangeState(GuardStateType.Stun);
                    AudioManager.Instance.PlaySFX(SFXType.SnekStun);
                    armed = false;
                    IsUsed = true;
                }
            }
        }
    }
}
