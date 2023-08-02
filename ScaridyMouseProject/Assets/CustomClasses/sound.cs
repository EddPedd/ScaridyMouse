using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Audio;

[System.Serializable]
public class sound 
{
    public string name;
    public AudioClip clip;

    [Range(0,1)]
    public float volume;
    [Range(-1, 3)]
    public float pitch;
    public bool loop;

    public bool isPlaying;

    public AudioSource source;
}
