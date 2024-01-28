using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McGuffin : MonoBehaviour
{
    // Script for a game object that is the win state
    [SerializeField] bool winOnGuardTouch;
    [SerializeField] GameObject completionObj;

    bool isComplete = false; //ToDo: Change graphic or something so the player knows this goal is completed

    public bool GetIsComplete() { return isComplete; }

    private void Start()
    {
        GameController.Instance.AddMcGuffin(this);
        completionObj.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (winOnGuardTouch)
        {
            if (other.tag == "Guard")
            {
                isComplete = true;
                completionObj.SetActive(true);
                GameController.Instance.CheckWin();
            }
        }
        else
        {
            if (other.tag == "Player")
            {
                isComplete = true;
                completionObj.SetActive(true);
                GameController.Instance.CheckWin();
            }
        }
    }
}
