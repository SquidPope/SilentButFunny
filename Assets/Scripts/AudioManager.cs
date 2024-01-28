using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFXType {Alert, BananaSlip, SnekStun, TeethChatter, WhoopieCushion}
public class AudioManager : MonoBehaviour
{
    // Manage audio
    [SerializeField] List<AudioSource> sfxSources;

    static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = GameObject.FindGameObjectWithTag("AudioManager");
                instance = go.GetComponent<AudioManager>();
            }

            return instance;
        }
    }

    public void PlaySFX(SFXType type)
    {
        sfxSources[(int)type].Play();
    }

    //ToDo: Testing code, remove
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.B))
        {
            PlaySFX(SFXType.BananaSlip);
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            PlaySFX(SFXType.SnekStun);
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            PlaySFX(SFXType.WhoopieCushion);
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            PlaySFX(SFXType.TeethChatter);
        }

        if (Input.GetKeyUp(KeyCode.H))
        {
            PlaySFX(SFXType.Alert);
        }
    }
}
