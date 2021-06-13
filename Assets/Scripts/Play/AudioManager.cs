using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private bool isInited;

    private void Awake()
    {
        if (!isInited) Init();
    }

    public void Init(){
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }

        isInited = true;
    }
    private void Start()
    {
        Sound s = Array.Find(sounds, sound => sound.clip);
        if (s.source.playOnAwake)
        {
            s.source.Play();
        }
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
        if(s == null)
        {
            Debug.Log("the sound" + name + "Has not been found");
            return;
        }
    }
    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
        if (s == null)
        {
            Debug.Log("the sound" + name + "Has not been found");
            return;
        }
    }
    public void SetAllsound(bool a)
    {
        if (a)
        {
            foreach (Sound s in sounds)
            {
                if(s.source == null){
                    Debug.Log("Null");
                    continue;
                }
                    

                s.source.pitch = 1;
            }
        }
        else if(!a)
        {
            foreach (Sound s in sounds)
            {
                if(s.source == null){
                    Debug.Log("Null");
                    continue;
                }

                s.source.pitch = 0;
            }
        }
    }
    public void SetSound(bool a, string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (a)
        {
            s.source.pitch = 1;
        }
        else if (!a)
        {
            s.source.pitch = 0;
        }
    }
}