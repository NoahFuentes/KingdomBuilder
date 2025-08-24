using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    [SerializeField] private string[] resNames;
    [SerializeField] private int[] resCounts;
    public int[] resCountMaxes;

    

    private void Awake()
    {
        Instance = this;
    }

    public void addResource(string resName, int amt)
    {
        for (int i = 0; i < resNames.Length; i++)
        {
            if (resName != resNames[i]) continue;
            NotificationManager.Instance.ShowResourceNotification(UIManager.Instance.GetResIconByName(resName), amt);
            resCounts[i] = Mathf.Clamp(resCounts[i] + amt, 0, resCountMaxes[i]);
            UIManager.Instance.UpdatePlayerInventoryResourceCount(i, resCounts[i], resCountMaxes[i]);
            return;
        }
    }

    public void depotToKingdom()
    {
        KingdomStats.Instance.AddResources(resNames, resCounts);
        for(int i = 0; i < resCounts.Length; i++)
        {
            resCounts[i]=0;
            UIManager.Instance.UpdatePlayerInventoryResourceCount(i, resCounts[i], resCountMaxes[i]);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) addResource("water", resCountMaxes[0]);
        if (Input.GetKeyDown(KeyCode.Alpha2)) addResource("food", resCountMaxes[1]);
        if (Input.GetKeyDown(KeyCode.Alpha3)) addResource("timber", resCountMaxes[2]);
        if (Input.GetKeyDown(KeyCode.Alpha4)) addResource("rough stone", resCountMaxes[3]);
        if (Input.GetKeyDown(KeyCode.Alpha5)) addResource("copper ore", resCountMaxes[4]);
        if (Input.GetKeyDown(KeyCode.Alpha6)) addResource("iron ore", resCountMaxes[5]);
        if (Input.GetKeyDown(KeyCode.Alpha7)) addResource("gold ore", resCountMaxes[6]);
        if (Input.GetKeyDown(KeyCode.Alpha8)) addResource("blood shard", resCountMaxes[7]);
        if (Input.GetKeyDown(KeyCode.Alpha9)) addResource("geode", resCountMaxes[8]);
        if (Input.GetKeyDown(KeyCode.Alpha0)) addResource("graven scrap", resCountMaxes[9]);
        if (Input.GetKeyDown(KeyCode.Y)) addResource("yellow essence", resCountMaxes[10]);
        if (Input.GetKeyDown(KeyCode.U)) addResource("blue essence", resCountMaxes[11]);
        if (Input.GetKeyDown(KeyCode.I)) addResource("red essence", resCountMaxes[12]);
        if (Input.GetKeyDown(KeyCode.O)) addResource("white essence", resCountMaxes[13]);
        if (Input.GetKeyDown(KeyCode.P)) addResource("artifact", resCountMaxes[14]);
    }

    
}
