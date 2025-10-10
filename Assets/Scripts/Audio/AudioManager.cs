using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public void PlaySoundByName(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (null != s)
        {
            if (s.pitchVariation)
                s.source.pitch = UnityEngine.Random.Range(0.85f, 1.15f);
            s.source.Play();
        }
    }


    public void Awake()
    {
        foreach (Sound s in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = s.clip;
            source.volume = s.volume;
            source.pitch = s.pitch;
            source.playOnAwake = s.playOnAwake;
            source.loop = s.loop;
            source.spatialBlend = s.spatialBlend;
            source.rolloffMode = AudioRolloffMode.Logarithmic;
            source.maxDistance = s.maxDistance;
            source.dopplerLevel = 0;
            s.source = source;
            if (source.playOnAwake) source.Play();
        }

    }
}