using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Script to manage level
    [SerializeField] List<GameObject> levels;

    void Awake()
    {
        foreach (GameObject level in levels)
        {
            level.SetActive(false);
        }

        int rand = Random.Range(0, levels.Count);
        levels[rand].SetActive(true);
    }
}
