using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] mobs;
    public ushort[] spawnCounts;
    [SerializeField] private ushort[] maxSpawns;

    [SerializeField] private float width, height, playerRadius, checkTime;
    private float lastCheckedTime;

    [SerializeField] private LayerMask lm;

    private void FixedUpdate()
    {
        if(Time.time - lastCheckedTime >= checkTime)
        {
            lastCheckedTime = Time.time;
            if(Physics.CheckSphere(transform.position, playerRadius, lm))
            {
                SpawnMobs();
            }
        }
    }

    private void SpawnMobs()
    {
        for (int i = 0; i < mobs.Length; i++)
        {
            int amountToSpawn = maxSpawns[i] - spawnCounts[i];
            for (int j = 0; j < amountToSpawn; j++)
            {
                float randX = Random.Range(-width / 2, width / 2);
                float randZ = Random.Range(-height / 2, height / 2);
                Vector3 spawnLoc = new Vector3(transform.position.x + randX, transform.position.y, transform.position.z + randZ);
                GameObject npc = Instantiate(mobs[i], spawnLoc, Quaternion.identity, gameObject.transform);
                npc.GetComponent<MobAI>().spawner = this;
                npc.GetComponent<MobAI>().spawnerIndex = (ushort)i;
                spawnCounts[i]++;
            }
        }
    }
}
