﻿using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private static bool isMuted = false;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
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
        if (!isMuted)
        {
            Play("song");
        }
    }

    // Update is called once per frame
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        try
        {
            s.source.Play();
        } catch (Exception e)
        {
            Debug.Log("No Sound to be Played " + e);
        }
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        try
        {
            s.source.Stop();
        }
        catch (Exception e)
        {
            Debug.Log("No Sound to be Stopped " + e);
        }
    }

    public void toggleMute(bool muted)
    {
        isMuted = muted;
        if (isMuted)
        {
            Play("song");
        }
    }
}
//How to call a sound in a script:
//FindObjectOfType<AudioManager>().Play("SoundName");