using UnityEngine;

public class KingdomStats : MonoBehaviour
{
    public static KingdomStats Instance { get; private set; }

    //Location definition
    public ushort m_KingdomLevel; // (Campsite, Town Center, Grand Courtyard, Great Hall, High Keep)
    public float m_KingdomRadius;

    public int kingdomPopulationCap;

    //Resources

    public string[] resourceNames;
    public int[] resourceCurrentAmounts;
    public int[] resourceMaxAmounts;

    public string[] upkeepResourceNames;
    public int[] upkeepAmounts;


    private void Awake()
    {
        Instance = this;
    }

    public bool CanAfford(string[] resources, int[] costs)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            bool canAfford = false;
            bool found = false;
            for (int j = 0; j < resourceNames.Length; j++)
            {
                if (resources[i] == resourceNames[j])
                {
                    found = true;
                    canAfford = (resourceCurrentAmounts[j] - costs[i] >= 0) ? true : false;
                    break;
                }
            }
            if (!found) Debug.Log("COULD NOT FIND RESOURCE OF NAME: " + resources[i]);
            if (!canAfford) return false;
        }
        return true;
    }
    public void RemoveResources(string[] resources, int[] costs)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            bool found = false;
            for (int j = 0; j < resourceNames.Length; j++)
            {
                if (resources[i] == resourceNames[j])
                {
                    found = true;
                    resourceCurrentAmounts[j] = Mathf.Clamp(resourceCurrentAmounts[j] - costs[i], 0, resourceMaxAmounts[j]);
                    break;
                }
            }
            if (!found) Debug.Log("COULD NOT FIND RESOURCE OF NAME: " + resources[i]);
        }
        UIManager.Instance.UpdateKingdomResourceCounts();
    }
    public void AddResources(string[] resources, int[] amts)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            bool found = false;
            for (int j = 0; j < resourceNames.Length; j++)
            {
                if (resources[i] == resourceNames[j])
                {
                    found = true;
                    resourceCurrentAmounts[j] = Mathf.Clamp(resourceCurrentAmounts[j] + amts[i], 0, resourceMaxAmounts[j]);
                    break;
                }
            }
            if (!found) Debug.Log("COULD NOT FIND RESOURCE OF NAME: " + resources[i]);
        }
        UIManager.Instance.UpdateKingdomResourceCounts();
    }

    private void Update()
    {
        string[] resArray = { "cloth", "iron"};
        int[] resAmts = { 20, 20 };
        if (Input.GetKeyDown(KeyCode.H)) AddResources(resArray, resAmts);
    }

    public bool waterBearerPresent;
    public bool farmerPresent;
    public bool builderPresent;
    public bool cookPresent;
    public bool lumberJackPresent;
    public bool soldier1Present;
    public bool blacksmithPresent;
    public bool minerPresent;
    public bool soldier2Present;
    public bool SorcererPresent;


}
