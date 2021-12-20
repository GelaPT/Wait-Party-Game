using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(1f, 2f)]
    public float pitch;
    public bool loop;

    [HideInInspector] public AudioSource source;

    public void BuildAudioSource(AudioSource source)
    {
        this.source = source;
        this.source.clip = clip;
        this.source.volume = volume;
        this.source.pitch = pitch;//Random.Range(-pitch, pitch);
        this.source.loop = loop;
    }
}