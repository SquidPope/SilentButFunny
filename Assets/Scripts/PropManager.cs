using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{
    // Keep track of prop pools
    List<List<Prop>> propLists;
    [SerializeField] List<GameObject> propPrefabs;

    [SerializeField] List<int> maxProps; //Per type

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
    }

    List<Prop> GenerateObjectPool(int id)
    {
        List<Prop> list = new List<Prop>();
        Debug.Log($"Max props for {id} is {maxProps[id]}");
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
        //find an inactive prop of the right type
        
        //if there isn't one, let the player know they reached their max
        //put the prop in the right spot
        //activate it
    }
}
