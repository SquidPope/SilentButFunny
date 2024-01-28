using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhoopieCushionProp : Prop
{
    // Prop that repels nearby guards
    float timer;
    float duration = 15f;

    protected override void GuardEnteredTrigger(GameObject guardObj)
    {
        base.GuardEnteredTrigger(guardObj);
        if (!IsActive)
        {
            IsActive = true;
            timer = 0f;
        }

        Guard g = nearbyGuards.Find(x => x.gameObject == guardObj);
        g.ChangeState(GuardStateType.Repel);

        AudioManager.Instance.PlaySFX(SFXType.WhoopieCushion);
    }

    protected override void GuardExitedTrigger(GameObject guardObj)
    {
        base.GuardExitedTrigger(guardObj);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            IsActive = false;
        }
    }
}
