using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public sound[] sounds;

    public static AudioManagerScript instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            GameObject.Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        foreach (sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;   
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Can´t play the sound " + name + " because can´t find it in the sounds array!");
        }
        else 
        {
            Debug.Log("Started playing the sound " + name);
            s.source.Play();
            s.isPlaying = true;
        }
    }

    public void Stop(string name)
    {
        sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Can´t stop playing the sound " + name + " because can´t find it in the sounds array!");
        }

        Debug.Log("Stopped playing the sound " + name);
        s.source.Stop();
        s.isPlaying = false;

    }
}
