using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McGuffin : MonoBehaviour
{
    // Script for a game object that is the win state
    [SerializeField] bool winOnGuardTouch;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (winOnGuardTouch)
        {
            if (other.tag == "Guard")
            {
                GameController.Instance.State = GameState.Win;
            }
        }
        else
        {
            if (other.tag == "Player")
            {
                GameController.Instance.State = GameState.Win;
            }
        }
    }
}
