using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Playing, Over, Win}
public class GameController : MonoBehaviour
{
    // Control the game state
    GameState state;

    static GameController instance;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.FindGameObjectWithTag("GameController");
                instance = go.GetComponent<GameController>();
            }

            return instance;
        }
    }

    public GameState State
    {
        get { return state; }
        set
        {
            state = value;
            Debug.Log($"State changed to {state}");
        }
    }

    private void Start()
    {
        State = GameState.Playing;
    }
}
