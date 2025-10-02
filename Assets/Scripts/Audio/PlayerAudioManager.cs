using UnityEngine;

public class PlayerAudioManager : AudioManager
{
    public static PlayerAudioManager Instance;

    private new void Awake()
    {
        Instance = this;
        base.Awake();
    }
}
