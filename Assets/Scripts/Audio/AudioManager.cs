using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private List<Sound> sounds;

    protected override void Awake()
    {
        base.Awake();
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
    }
}
