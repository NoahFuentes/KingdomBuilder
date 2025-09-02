using UnityEngine;

public class MobAudioManager : AudioManager
{
    [SerializeField] private AudioClip[] footSteps;
    [SerializeField][Range(0f, 1f)] private float footStepVolume = 1f;
    private AudioSource footStepSource;

    public void PlayFootStep()
    {
        if (footSteps.Length <= 0) return;
        int step = Random.Range(0, footSteps.Length);
        float pitch = Random.Range(0.5f, 1.5f);
        footStepSource.clip = footSteps[step];
        footStepSource.pitch = pitch;
        if (null != footSteps[step]) footStepSource.Play();
    }

    private new void Awake()
    {
        footStepSource = gameObject.AddComponent<AudioSource>();
        footStepSource.volume = footStepVolume;
        footStepSource.spatialBlend = 1f;
        footStepSource.rolloffMode = AudioRolloffMode.Linear;
        footStepSource.maxDistance = 110f;
        base.Awake();
    }
}
