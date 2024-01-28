using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameState {Menu, Playing, Over, Win}
public class GameStateChange : UnityEvent<GameState> { }
public class GameController : MonoBehaviour
{
    // Control the game state
    GameState state;

    List<McGuffin> mcGuffins;

    GameStateChange stateChange = new GameStateChange();
    public GameStateChange StateChange { get { return stateChange; } }

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
            //ToDo: If value is win, make sure all active goals are finished
            state = value;
            Debug.Log($"State changed to {state}");
            StateChange.Invoke(state);
        }
    }

    private void Awake()
    {
        mcGuffins = new List<McGuffin>();
    }

    private void Start()
    {
        State = GameState.Playing;
    }

    public void AddMcGuffin(McGuffin mcGuffin)
    {
        mcGuffins.Add(mcGuffin);
    }

    public void CheckWin()
    {
        foreach (McGuffin mg in mcGuffins)
        {
            if (!mg.GetIsComplete())
                return;
        }

        //They're all complete, the player wins!
        State = GameState.Win;
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (State == GameState.Playing)
            {
                State = GameState.Menu;
            }
            else if (State == GameState.Menu)
            {
                State = GameState.Playing;
            }
        }
    }
}
