using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{
    // Keep track of prop pools
    List<List<Prop>> propLists;
    [SerializeField] List<GameObject> propPrefabs;

    [SerializeField] List<int> maxProps; //Per type

    //ToDo: Print this out for the player on win/death?
    int[] propUses; //Keeps track of how many times the player used each prop.

    GameState currentState;

    static PropManager instance;
    public static PropManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.FindGameObjectWithTag("PropManager");
                instance = go.GetComponent<PropManager>();
            }

            return instance;
        }
    }

    void Start()
    {
        propLists = new List<List<Prop>>();
        for (int i = 0; i <= (int)PropType.None - 1; i++) //- 1 because None is not a valid type, it's just the total.
        {
            List<Prop> list = GenerateObjectPool(i);
            propLists.Add(list);
        }

        GameController.Instance.StateChange.AddListener(StateChange);
        currentState = GameState.Playing; //Make sure that we know we're playing, since this sometimes gets set before the listener is attached.

        propUses = new int[(int)PropType.None - 1];
    }

    public void StateChange(GameState state)
    {
        currentState = state;
    }

    List<Prop> GenerateObjectPool(int id)
    {
        List<Prop> list = new List<Prop>();
        for (int i = 0; i <= maxProps[id]; i++)
        {
            Prop p = GameObject.Instantiate(propPrefabs[id], transform.position, Quaternion.identity).GetComponent<Prop>();
            p.Init();
            p.IsActive = false;
            list.Add(p);
        }

        return list;
    }

    public void SetProp(PropType type, Vector3 position)
    {
        Debug.Log($"State is {currentState}");
        if (currentState != GameState.Playing)
            return;

        //find an inactive prop of the right type
        Prop prop = propLists[(int)type].Find(x => x.IsActive == false);

        if (prop == null)
        {
            //we used them all
            //ToDo: Tell the player that
            return;
        }
        
        //put the prop in the right spot
        prop.SetPosition(position);

        //activate it
        prop.IsActive = true;

        propUses[(int)type]++;
    }
}
