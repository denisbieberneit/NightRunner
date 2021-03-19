using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SocialPlatforms;

[System.Serializable]
public class Sound
{
    public AudioClip clip;

    public string name;

    [Range(0f, 1f)]
    public float volumne;

    [Range(0.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;

    public bool loop;
    public bool isActive;

}
