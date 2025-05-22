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
}
