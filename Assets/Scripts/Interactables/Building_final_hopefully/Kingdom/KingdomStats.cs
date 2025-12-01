using UnityEngine;

public class KingdomStats : MonoBehaviour
{
    public static KingdomStats Instance { get; private set; }

    //Resources

    public string[] resourceNames;
    public int[] resourceCurrentAmounts;
    public int[] resourceMaxAmounts;


    //Buildings
    public bool[] buildingsRestored; // THESE BUILDINGS *MUST* FOLLOW THE ORDER OF CompanionManager.companionTitles!!!
    /*houses, gate, all buildings that dont belong to a companion - 0
     * well - 1
     * farm - 2
     * lumber mill - 3...
     * masonry yard
     * stable
     * forge
     * weaponsmithy
     * bowyers workshop
     * sanctum
     * armory
     * quarry
     */

    public int maxPopulation; // when a house is restored, this goes up one
    public int currentPopulation; // when a companion joins the city, this goes up one. Cannot exceed maxPopulation


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
            int tempAmt = 0;
            for (int j = 0; j < resourceNames.Length; j++)
            {
                if (resources[i] == resourceNames[j])
                {
                    found = true;
                    canAfford = (resourceCurrentAmounts[j] - costs[i] >= 0) ? true : false;
                    tempAmt = resourceCurrentAmounts[j];
                    break;
                }
            }
            if (!found) Debug.Log("COULD NOT FIND RESOURCE OF NAME: " + resources[i]);
            if (!canAfford)
            {
                Debug.Log("COULD NOT AFFORD RESOURCE OF NAME: " + resources[i] + ". HAVE: " + tempAmt + " NEED: " + costs[i]);
                return false;
            }
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
}
