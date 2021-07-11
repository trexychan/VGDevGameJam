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

    void Start()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume",1f);
        ChangeVolume(masterVolume);
    }

    public void ChangeVolume(float newVolume)
    {
        PlayerPrefs.SetFloat("MasterVolume", newVolume);

        foreach (AudioSource source in audioSources)
        {
            source.volume = newVolume;
        }
    }
}
