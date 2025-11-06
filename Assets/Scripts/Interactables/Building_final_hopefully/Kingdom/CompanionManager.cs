using UnityEngine;

public class CompanionManager : MonoBehaviour
{
    public static CompanionManager Instance;

    [SerializeField] private GameObject[] companionPrefabs; // list must be in same order as kingdom stat lists
    [SerializeField] private Transform companionSpawnPoint;

    private float waitTime;
    private float lastWaitStart;

    private KingdomStats ks;

    private void Start()
    {
        ks = KingdomStats.Instance;
    }
    private void FixedUpdate()
    {
        if(Time.time - lastWaitStart > waitTime)
        {
            //check if the game time is correct for companion joining (day time)
            //check if there is population space
            if (ks.currentPopulation + 1 > ks.maxPopulation) return;
            //check if a companion can join / work at their place
            for(int i = 0; i < ks.buildingsRestored.Length; i++)
            {
                if (ks.companionsPresent[i]) continue; //next iteration if companion is present
                if (!ks.buildingsRestored[i]) continue; //next iteration if building is not restored
                if (!ks.companionsSaved[i]) continue; //next iteration if companion is not saved
                //spawn companion if so
                Instantiate(companionPrefabs[i], companionSpawnPoint.position, Quaternion.identity);

            }
            //select random wait time
            waitTime = Random.Range(30f, 90f);
            lastWaitStart = Time.time;
        }
        
    }
}
