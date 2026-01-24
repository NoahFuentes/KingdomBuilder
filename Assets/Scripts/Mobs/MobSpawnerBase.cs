using UnityEngine;
//using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class MobSpawnerBase : MonoBehaviour
{
    [SerializeField] private GameObject mobToSpawn;

    [SerializeField] private List<Transform> mobSpawnTransforms; //this count is also the amount of mobs the spawner will have
    [SerializeField] private List<GameObject> mobs;

    [SerializeField] private float setActiveDistance; //Distance the player must be to enable/disable all mobs
    private bool playerIsInRange;

    [SerializeField] private float respawnTime;  //Time delta required since lastKillTime for mobs to respawn
    [HideInInspector] public float lastKillTime; //Time.time when last mob was killed

    [HideInInspector] public bool needRespawning; //Is set to true in the DIE state of a mob

    [HideInInspector] public UnityAction<int> respawnedMobs; //other classes register functions with this
    [HideInInspector] public UnityAction mobDied;       //other classes register functions with this

    private void SpawnMobs() //spawns a mobToSpawn at each mobSpawnPosition
    {
        GameObject mob;
        for (int i = 0; i < mobSpawnTransforms.Count; i++)
        {
            mob = Instantiate(mobToSpawn, mobSpawnTransforms[i].position, mobSpawnTransforms[i].rotation, transform);
            mob.GetComponent<MobStats>().spawner = this;
            mob.GetComponent<MobStats>().spawnPoint = mobSpawnTransforms[i].position;
            mobs.Add(mob);
        }
        respawnedMobs?.Invoke(mobSpawnTransforms.Count);
    }

    private void ResetAllMobs()
    {
        for(int i = 0; i < mobs.Count; i++)
        {
            GameObject mob = mobs[i];
            MobBase mobBase = mob.GetComponent<MobBase>();
            mob.transform.position = mobSpawnTransforms[i].position;
            mob.transform.rotation = mobSpawnTransforms[i].rotation;
            mobBase.agent.Warp(mob.transform.position);
            mobBase.agent.SetDestination(mobBase.stats.spawnPoint);
            mobBase.stateMachine.ChangeState(mobBase.defaultState);
        }
        respawnedMobs?.Invoke(mobSpawnTransforms.Count);
    }

    private void SetMobsActiveState(bool isActive)
    {
        foreach (GameObject mob in mobs)
        {
            //mobBase
            mob.GetComponent<MobBase>().enabled = isActive;
            //animator
            mob.GetComponent<Animator>().enabled = isActive;
            //renderer
            foreach(MeshRenderer mr in mob.GetComponentsInChildren<MeshRenderer>())
                mr.enabled = isActive;
        }
    }

    public void ReportMobDeath()
    {
        needRespawning = true;
        lastKillTime = Time.time;
        mobDied?.Invoke();
    }

    //UNITY FUNCTIONS

    /*
    private void Start()
    {
        foreach(Transform t in GetComponentInChildren<Transform>())
        {
            mobSpawnTransforms.Add(t);
        }
        GetComponent<SphereCollider>().radius = setActiveDistance;

        SpawnMobs();
    }
    */
    private void Start()
    {
        foreach (Transform t in GetComponentInChildren<Transform>())
        {
            mobSpawnTransforms.Add(t);
        }
        GetComponent<SphereCollider>().radius = setActiveDistance;

        SpawnMobs();

        if (Vector3.Distance(PlayerStats.Instance.transform.position, transform.position) <= setActiveDistance)
        {
            SetMobsActiveState(true);
            playerIsInRange = true;
        }
        else
        {
            SetMobsActiveState(false);
            playerIsInRange = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerIsInRange = true;
        SetMobsActiveState(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerIsInRange = false;
        SetMobsActiveState(false);
    }

    private void FixedUpdate()
    {
        if (!playerIsInRange && needRespawning && Time.time - lastKillTime >= respawnTime)
        {
            needRespawning = false;
            ResetAllMobs();
        }
    }
}
