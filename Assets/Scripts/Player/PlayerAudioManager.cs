using UnityEngine;
using System;

public class PlayerAudioManager : MonoBehaviour
{
    public static PlayerAudioManager Instance;

    [SerializeField] private AudioClip[] footSteps;
    private AudioSource footStepSource;

    [SerializeField] private Sound[] playerSounds;

    public void PlayFootStep()
    {
        int step = UnityEngine.Random.Range(0, footSteps.Length);
        float pitch = UnityEngine.Random.Range(0.5f, 1.5f);
        footStepSource.clip = footSteps[step];
        footStepSource.pitch = pitch;
        if (null != footSteps[step]) footStepSource.Play();
    }

    public void PlayClipByName(string clipName)
    {
        Sound s = Array.Find(playerSounds, sound => sound.name == clipName);
        if (null != s)
            s.source.Play();
    }


    private void Awake()
    {
        Instance = this;
        footStepSource = GetComponent<AudioSource>();
        foreach (Sound s in playerSounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = s.clip;
            source.volume = s.volume;
            source.pitch = s.pitch;
            s.source = source;
        }
    }
}
