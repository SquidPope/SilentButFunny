using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardState
{
    // Base class for a guard state- controls guard's current behavior
    protected Guard guard;

    public virtual void EnterState() { }
    public virtual void Tick() { }
    public virtual void EndState() { }
}
