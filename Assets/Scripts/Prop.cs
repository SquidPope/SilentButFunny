using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PropType {Banana, Snek, Teeth, WhoopieCushion, None}
public class Prop : MonoBehaviour
{
    // Object placed by a player, consists of a trigger area around a sprite
    protected List<Guard> nearbyGuards;

    [SerializeField] SpriteRenderer renderer;
    Vector3 startPos;

    bool isActive;
    public bool IsActive //PROPerty lol
    {
        get { return isActive; }
        set
        {
            isActive = value;

            if (renderer != null)
                renderer.enabled = value;

            if (!isActive)
                transform.position = startPos;
        }
    }

    public void SetPosition(Vector3 pos) { transform.position = pos; }

    public virtual void Init()
    {
        nearbyGuards = new List<Guard>();
        startPos = transform.position;
    }

    protected virtual void GuardEnteredTrigger(GameObject guardObj)
    {
        Guard g = guardObj.GetComponent<Guard>();
        nearbyGuards.Add(g);
    }
    protected virtual void GuardExitedTrigger(GameObject guardObj)
    {
        Guard g = guardObj.GetComponent<Guard>();
        nearbyGuards.Remove(g);
    }

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
