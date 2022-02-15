using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AudioManager : Singleton<AudioManager>
{
    public Sound[] soundsArray;
    private readonly List<Sound> sounds = new();

    protected override void Awake()
    {
        base.Awake();
        foreach (var sound in soundsArray)
        {
            sounds.Add(sound);
        }

        foreach(var sound in sounds)
        {
            sound.BuildAudioSource(gameObject.AddComponent<AudioSource>());
        }
    }

    public Sound PlaySound(string soundName)
    {
        Sound sound;
        if ((sound = sounds.FirstOrDefault(s => s.name == soundName)) == null) return null;

        sound.source.Play();

        return sound;
    }

    public void StopSound(string soundName)
    {
        Sound sound;
        if ((sound = sounds.FirstOrDefault(s => s.name == soundName)) == null) return;

        sound.source.Stop();
    }

    public void PauseSound(string soundName)
    {
        Sound sound;
        if ((sound = sounds.FirstOrDefault(s => s.name == soundName)) == null) return;

        sound.source.Pause();
    }

    public void StopAnySound()
    {
        foreach (Sound sound in sounds)
        {
            if (sound.source != null)
            {
                sound.source.Stop();
            }
        }
    }

    public void PauseAnySound()
    {
        foreach (Sound sound in sounds)
        {
            if (sound.source != null)
            {
                sound.source.Pause();
            }
        }
    }

    public void ResumeAnySound()
    {
        foreach (Sound sound in sounds)
        {
            if (sound.source != null)
            {
                sound.source.UnPause();
            }
        }
    }
}
