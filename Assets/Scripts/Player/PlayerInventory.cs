using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    [SerializeField] private string[] resNames;
    [SerializeField] private int[] resCounts;
    [SerializeField] private int[] resCountMaxes;

    public void addResource(string resName, int amt)
    {
        for (int i = 0; i < resNames.Length; i++)
        {
            if (resName != resNames[i]) continue;
            resCounts[i] = (((resCounts[i] += amt) > resCountMaxes[i]) ? resCountMaxes[i] : resCounts[i]);
        }
    }

    public void depotToKingdom()
    {
        KingdomStats.Instance.AddResources(resNames, resCounts);
        for(int i = 0; i < resCounts.Length; i++)
            resCounts[i]=0;
    }

    private void Start()
    {
        Instance = this;
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
        if (Input.GetKeyDown(KeyCode.Alpha8)) addResource("crystal", 1);
        if (Input.GetKeyDown(KeyCode.Alpha9)) addResource("black crystal", 1);
    }
}
