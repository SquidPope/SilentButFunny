using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaProp : Prop
{
    // Prop that makes guards enter a sliding state on contact- should deactivate afterwards?
    Guard guard;

    protected override void GuardEnteredTrigger(GameObject guardObj)
    {
        if (IsActive)
        {
            guard = guardObj.GetComponent<Guard>();
            guard.ChangeState(GuardStateType.Slide);
            IsActive = false;

            AudioManager.Instance.PlaySFX(SFXType.BananaSlip);
        }
    }
}
