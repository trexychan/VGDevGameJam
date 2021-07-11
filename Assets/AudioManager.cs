using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioSource> audioSources;

    private static AudioManager _instance;
    void Awake()
    {
        // lazy
        _instance = this;
    }

    public static AudioManager GetInstance()
    {
        return _instance;
    }

    public void ChangeVolume(float newVolume)
    {
        foreach (AudioSource source in audioSources)
        {
            source.volume = newVolume;
        }
    }
}
