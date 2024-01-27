using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PropType {Banana, Snek, Teeth, WhoopieCushion, None}
public class Prop : MonoBehaviour
{
    // Object placed by a player, consists of a trigger area around a sprite
    PropType currentType;
    List<Guard> nearbyGuards;

    [SerializeField] SpriteRenderer renderer;

    bool isActive;
    public bool IsActive //PROPerty lol
    {
        get { return isActive; }
        set
        {
            isActive = value;
            
            if (renderer != null)
                renderer.enabled = value;
        }
    }

    public void SetType(PropType type)
    {
        currentType = type; //ToDo: Set behavior
    }

    public virtual void Init(){}

    protected virtual void GuardEnteredTrigger(GameObject guardObj) {}
    protected virtual void GuardExitedTrigger(GameObject guardObj) {}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsActive)
            return;

        if (other.tag == "Guard")
        {
            //activate or something, idk
            GuardEnteredTrigger(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!IsActive)
            return;

        if (other.tag == "Guard")
        {
            GuardExitedTrigger(other.gameObject);
        }
    }
}
