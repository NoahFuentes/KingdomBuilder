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
        if (Input.GetKeyDown(KeyCode.Alpha1)) addResource("food", 15);
        if (Input.GetKeyDown(KeyCode.Alpha2)) addResource("water", 15);
        if (Input.GetKeyDown(KeyCode.Alpha3)) addResource("wood", 10);
        if (Input.GetKeyDown(KeyCode.Alpha4)) addResource("textile", 10);
        if (Input.GetKeyDown(KeyCode.Alpha5)) addResource("stone", 5);
        if (Input.GetKeyDown(KeyCode.Alpha6)) addResource("iron", 5);
        if (Input.GetKeyDown(KeyCode.Alpha7)) addResource("gold", 3);
    }

    
}
