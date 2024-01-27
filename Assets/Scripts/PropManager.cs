using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{
    // Keep track of prop pools
    [SerializeField] GameObject bananaPrefab;
    List<BananaProp> bananas;
    int maxBananas = 7;

    [SerializeField] GameObject snekPrefab;
    List<SnekProp> sneks;
    int maxSneks = 5;

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
        bananas = new List<BananaProp>();
        for (int i = 0; i < maxBananas; i++)
        {
            BananaProp p = GameObject.Instantiate(bananaPrefab, transform.position, Quaternion.identity).GetComponent<BananaProp>();
            p.Init();
            p.IsActive = false;
            bananas.Add(p);
        }

        sneks = new List<SnekProp>();
        for (int i = 0; i < maxSneks; i++)
        {
            SnekProp p = GameObject.Instantiate(snekPrefab, transform.position, Quaternion.identity).GetComponent<SnekProp>();
            p.Init();
            p.IsActive = false;
            sneks.Add(p);
        }
    }

    public void SetProp(PropType type, Vector3 position)
    {
        //find an inactive prop of the right type
        //if there isn't one, let the player know they reached their max
        //put the prop in the right spot
        //activate it
    }
}
