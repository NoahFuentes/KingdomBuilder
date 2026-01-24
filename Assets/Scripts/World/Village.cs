using UnityEngine;
using System.Collections.Generic;

public class Village : MonoBehaviour
{
    [SerializeField] private string companionToSave; //Lumber Jack, Farmer, etc

    [SerializeField] private int mobsAlive;

    private MobSpawnerBase[] spawners;


    private void TrackMobDeath()
    {
        mobsAlive--;
        if (mobsAlive <= 0)
        {
            CompanionManager.Instance.SetCompanionAsSaved(companionToSave);
            foreach(MobSpawnerBase spawner in spawners)
                Destroy(spawner);
            Destroy(GetComponent<SphereCollider>());
        }
    }

    private void AddMobsAlive(int mobCount)
    {
        mobsAlive += mobCount;
    }

    //UNITY FUNCTIONS

    private void Awake()
    {
        spawners = GetComponentsInChildren<MobSpawnerBase>();
        foreach (MobSpawnerBase spawner in spawners)
        {
            spawner.respawnedMobs += AddMobsAlive;
            spawner.mobDied += TrackMobDeath;
        }
    }

    [SerializeField] private KeyCode testinput;
    private void Update()
    {
        if (Input.GetKeyDown(testinput)) CompanionManager.Instance.SetCompanionAsSaved(companionToSave);
    }
}
