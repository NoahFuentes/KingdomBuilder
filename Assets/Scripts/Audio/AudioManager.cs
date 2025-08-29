using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource worldAmbience;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        worldAmbience = GetComponent<AudioSource>();
        worldAmbience.Play();
    }
}
