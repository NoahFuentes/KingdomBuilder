using UnityEngine;
using System.Collections;

public class CompanionManager : MonoBehaviour
{
    public static CompanionManager Instance;

    public string[] companionTitles;
    public bool[] companionsSaved;
    public bool[] companionsPresent;

    [SerializeField] private Transform[] companionWorkPositions;

    [SerializeField] private GameObject[] companionPrefabs; // list must be in same order as companionTitles above
    [SerializeField] private Transform companionSpawnPoint;

    private float waitTime;
    private float lastWaitStart;

    private KingdomStats ks;

    public void SetCompanionAsSaved(string title)
    {
        for (int i = 0; i < companionTitles.Length; i++)
        {
            if (companionTitles[i] == title)
            {
                companionsSaved[i] = true;
                return;
            }
        }
        Debug.Log("No companion found with title: " + title);
    }

    public void SendAllCompanionsHome() // This is to be called when the in game clock reaches end of work hours.
    {
        foreach (Companion companion in GameObject.FindObjectsByType<Companion>(FindObjectsSortMode.None))
        {
            StartCoroutine(StartGoHome(companion, Random.Range(0f, 30f)));
        }
    }
    private IEnumerator StartGoHome(Companion companion, float delay)
    {
        yield return new WaitForSeconds(delay);
        companion.stateMachine.ChangeState(companion.walkingHome);
    }

    public void WakeAllCompanionsUp() // This is to be called when the in game clock reaches start of work hours.
    {
        foreach (Companion companion in GameObject.FindObjectsByType<Companion>(FindObjectsSortMode.None))
        {
            StartCoroutine(StartGoWork(companion, Random.Range(0f, 30f)));
        }
    }
    private IEnumerator StartGoWork(Companion companion, float delay)
    {
        yield return new WaitForSeconds(delay);
        companion.stateMachine.ChangeState(companion.walkingWork);
    }

    //UNITY FUNCTIONS

    private void Awake()
    {
        Instance = this;
    }

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
                if (companionsPresent[i]) continue; //next iteration if companion is present
                if (!ks.buildingsRestored[i]) continue; //next iteration if building is not restored
                if (!companionsSaved[i]) continue; //next iteration if companion is not saved
                //spawn companion if so
                GameObject companionSpawned = Instantiate(companionPrefabs[i], companionSpawnPoint.position, Quaternion.identity);
                companionSpawned.GetComponent<Companion>().workPosition = companionWorkPositions[i];
                NotificationManager.Instance.Notify(companionSpawned.GetComponent<Companion>().info.companionName + " the " + companionTitles[i] + " has joined Center City!", Color.yellow);
                companionsPresent[i] = true;
                ks.currentPopulation++;
                break;
            }
            //select random wait time
            waitTime = Random.Range(30f, 90f);
            lastWaitStart = Time.time;
        }
        
    }
}
