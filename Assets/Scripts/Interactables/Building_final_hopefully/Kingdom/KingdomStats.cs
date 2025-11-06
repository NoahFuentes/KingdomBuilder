using UnityEngine;

public class KingdomStats : MonoBehaviour
{
    public static KingdomStats Instance { get; private set; }

    //Resources

    public string[] resourceNames;
    public int[] resourceCurrentAmounts;
    public int[] resourceMaxAmounts;


    //Buildings
    public bool[] buildingsRestored;
    /*gate
     * well
     * farm
     * lumber mill
     * masons yard
     * stable
     * forge
     * weaponsmithy
     * bowyers workshop
     * sanctum
     * armory
     * quarry
     */

    //Companions
    public bool[] companionsSaved;
    /*councilman
     * waterbearer
     * farmer
     * lumberjack
     * mason
     * stablemaster
     * blacksmith
     * swordsmith
     * fletcher
     * mage
     * armorer
     * miner
     */

    public bool[] companionsPresent; // same order as above

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

    /* might not need this
    public void SetBuildingRestored(string buildingName)
    {
        switch (buildingName)
        {
            case "Gate":
                gateIsRestored = true;
                break;
            case "Well":
                wellIsRestored = true;
                break;
            case "Farm":
                farmIsRestored = true;
                break;
            case "Lumber Mill":
                lumberMillIsRestored = true;
                break;
            case "Mason's Yard":
                masonsYardIsRestored = true;
                break;
            case "Stable":
                stableIsRestored = true;
                break;
            case "Forge":
                forgeIsRestored = true;
                break;
            case "Weaponsmithy":
                weaponsmithyIsRestored = true;
                break;
            case "Bowyer's Workshop":
                bowyersWorkshopIsRestored = true;
                break;
            case "Sanctum":
                sanctumIsRestored = true;
                break;
            case "Armory":
                armoryIsRestored = true;
                break;
            case "Quarry":
                quarryIsRestored = true;
                break;
            default:
                Debug.Log("No building of name: " + buildingName);
                break;
        }

    }
    */

    private void Update()
    {
        //Give kingdom resources
        string[] resArray = { "cloth", "iron"};
        int[] resAmts = { 20, 20 };
        if (Input.GetKeyDown(KeyCode.H)) AddResources(resArray, resAmts);
    }

    private void OnTriggerEnter(Collider other) //NOT A GOOD PLACE FOR THIS FUNCTION BUT IT HAS TO BE HERE
    {
        if (other.gameObject.tag == "Player")
        {
            //buildRangeIndicator.SetActive(true);
            UIManager.Instance.openKingdomOverlay();
        }
    }
    private void OnTriggerExit(Collider other)  //NOT A GOOD PLACE FOR THIS FUNCTION BUT IT HAS TO BE HERE
    {
        if (other.gameObject.tag == "Player")
        {
            //buildRangeIndicator.SetActive(false);
            UIManager.Instance.openExploringOverlay();
        }
    }

    


}
