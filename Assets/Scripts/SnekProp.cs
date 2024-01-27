using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekProp : Prop
{
    // Prop that stuns nearby guards after a delay
    List<Guard> guardsInTrigger;
    //ToDo: Countdown displaying over the object would be nice

    bool armed = false;
    float timer = 0f;
    float timerMax = 3f; //ToDo: this feels right, make sure guards aren't too fast for it though

    public override void Init()
    {
        guardsInTrigger = new List<Guard>();
    }

    protected override void GuardEnteredTrigger(GameObject guardObj)
    {
        guardsInTrigger.Add(guardObj.GetComponent<Guard>());
        armed = true;
    }

    protected override void GuardExitedTrigger(GameObject guardObj)
    {
        guardsInTrigger.Remove(guardObj.GetComponent<Guard>());
    }

    void Update()
    {
        if (armed)
        {
            timer += Time.deltaTime;
            if (timer >= timerMax)
            {
                //BOOM
                foreach (Guard g in guardsInTrigger)
                {
                    g.ChangeState(GuardStateType.Stun);
                    armed = false;
                }
            }
        }
    }
}
