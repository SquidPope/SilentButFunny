using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFXType { Alert, BananaSlip, Death, SnekStun, TeethChatter, WhoopieCushion}
public class AudioManager : MonoBehaviour
{
    // Manage audio
    [SerializeField] List<AudioSource> sfxSources;
    public void PlaySFX(SFXType type)
    {
        sfxSources[(int)type].Play();
    }
}
