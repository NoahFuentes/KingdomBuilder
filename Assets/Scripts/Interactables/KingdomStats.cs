using UnityEngine;

public class KingdomStats : MonoBehaviour
{
    //Location definition
    public ushort m_KingdomLevel; // (Campsite, Town Center, Grand Courtyard, Great Hall, High Keep)
    public float m_KingdomRadius;


    //Resources (Currently: Food, water, wood, textiles, stone, iron, gold, crystal, and black crystal) 
    public int m_MaxFoodAmount;
    public int m_CurrentFoodAmount;
    public int m_FoodConsumptionAmount;

    public int m_MaxWaterAmount;
    public int m_CurrentWaterAmount;
    public int m_WaterConsumptionAmount;

    public int m_MaxWoodAmount;
    public int m_CurrentWoodAmount;
    public int m_WoodConsumptionAmount;

    public int m_MaxTextileAmount;
    public int m_CurrentTextileAmount;

    public int m_MaxStoneAmount;
    public int m_CurrentStoneAmount;

    public int m_MaxIronAmount;
    public int m_CurrentIronAmount;

    public int m_MaxGoldAmount;
    public int m_CurrentGoldAmount;

    public int m_MaxCrystalAmount;
    public int m_CurrentCrystalAmount;

    public int m_MaxBlackCrystalAmount;
    public int m_CurrentBlackCrystalAmount;

    private UIManager ui;

    private void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
    }

    public bool CanAfford(string[] resources, int[] costs)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            switch (resources[i])
            {
                case "food":
                    if (m_CurrentFoodAmount - costs[i] < 0) return false;
                    break;
                case "water":
                    if (m_CurrentWaterAmount - costs[i] < 0) return false;
                    break;
                case "wood":
                    if (m_CurrentWoodAmount - costs[i] < 0) return false;
                    break;
                case "textile":
                    if (m_CurrentTextileAmount - costs[i] < 0) return false;
                    break;
                case "stone":
                    if (m_CurrentStoneAmount - costs[i] < 0) return false;
                    break;
                case "iron":
                    if (m_CurrentIronAmount - costs[i] < 0) return false;
                    break;
                case "gold":
                    if (m_CurrentGoldAmount - costs[i] < 0) return false;
                    break;
                case "crystal":
                    if (m_CurrentCrystalAmount - costs[i] < 0) return false;
                    break;
                case "black crystal":
                    if (m_CurrentBlackCrystalAmount - costs[i] < 0) return false;
                    break;
                default:
                    Debug.Log("Cannot find resource of type: " + resources[i]);
                    return false;
            }
        }
        return true;
    }
    public void RemoveResources(string[] resources, int[] costs)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            switch (resources[i])
            {
                case "food":
                    m_CurrentFoodAmount -= costs[i];
                    break;
                case "water":
                    m_CurrentWaterAmount -= costs[i];
                    break;
                case "wood":
                    m_CurrentWoodAmount -= costs[i];
                    break;
                case "textile":
                    m_CurrentTextileAmount -= costs[i];
                    break;
                case "stone":
                    m_CurrentStoneAmount -= costs[i];
                    break;
                case "iron":
                    m_CurrentIronAmount -= costs[i];
                    break;
                case "gold":
                    m_CurrentGoldAmount -= costs[i];
                    break;
                case "crystal":
                    m_CurrentCrystalAmount -= costs[i];
                    break;
                case "black crystal":
                    m_CurrentBlackCrystalAmount -= costs[i];
                    break;
                default:
                    Debug.Log("Cannot find resource of type: " + resources[i]);
                    break;
            }
        }
        ui.UpdateResourceCounts();
    }
    public void AddResources(string[] resources, int[] amts)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            switch (resources[i])
            {
                case "food":
                    m_CurrentFoodAmount = (((m_CurrentFoodAmount += amts[i]) > m_MaxFoodAmount) ? m_MaxFoodAmount : m_CurrentFoodAmount);
                    break;
                case "water":
                    m_CurrentWaterAmount = (((m_CurrentWaterAmount += amts[i]) > m_MaxWaterAmount) ? m_MaxWaterAmount : m_CurrentWaterAmount);
                    break;
                case "wood":
                    m_CurrentWoodAmount = (((m_CurrentWoodAmount += amts[i]) > m_MaxWoodAmount) ? m_MaxWoodAmount : m_CurrentWoodAmount);
                    break;
                case "textile":
                    m_CurrentTextileAmount = (((m_CurrentTextileAmount += amts[i]) > m_MaxTextileAmount) ? m_MaxTextileAmount : m_CurrentTextileAmount);
                    break;
                case "stone":
                    m_CurrentStoneAmount = (((m_CurrentStoneAmount += amts[i]) > m_MaxStoneAmount) ? m_MaxStoneAmount : m_CurrentStoneAmount);
                    break;
                case "iron":
                    m_CurrentIronAmount = (((m_CurrentIronAmount += amts[i]) > m_MaxIronAmount) ? m_MaxIronAmount : m_CurrentIronAmount);
                    break;
                case "gold":
                    m_CurrentGoldAmount = (((m_CurrentGoldAmount += amts[i]) > m_MaxGoldAmount) ? m_MaxGoldAmount : m_CurrentGoldAmount);
                    break;
                case "crystal":
                    m_CurrentCrystalAmount = (((m_CurrentCrystalAmount += amts[i]) > m_MaxCrystalAmount) ? m_MaxCrystalAmount : m_CurrentCrystalAmount);
                    break;
                case "black crystal":
                    m_CurrentBlackCrystalAmount = (((m_CurrentBlackCrystalAmount += amts[i]) > m_MaxBlackCrystalAmount) ? m_MaxBlackCrystalAmount : m_CurrentBlackCrystalAmount);
                    break;
                default:
                    Debug.Log("Cannot find resource of type: " + resources[i]);
                    break;
            }
        }
        ui.UpdateResourceCounts();
    }



    //population/NPC count
    public ushort m_MaxPopulation;
    public ushort m_CurrentPopulation;

 /*          Resource Management: (Can have multiple of each of these)
 *              Water Bearer(gathers water from a well, adds to kingdom supply)
 *              Farmer(gathers raw food and fabric from farm building the kingdom)
 *              Cook(turns raw food to edible food in a kitchen, adds to kingdom supply)
 *              Lumberjack(gathers wood near a lumber mill from the wilderness, adds to the kingdom supply)
 *              Miner(gathers stone, raw iron, raw gold, and crystal from the wilderness near a mine, adds to kingdom supply)
 *              
 *          Crafters: (Only one can be alive at a time)
 *              Artisan(Player interacts to craft general items at a workshop)
 *              BlackSmith(Turns raw metals into ingots.Player interacts to craft metalworks at a forge)
 *              Builder(Player interacts to build advanced buildings at a planning quarters)
 *              Mage(Player interacts to craft magic items at a mage tower)
 * 
 *          General:
 *              Soldier(Do I implement this? Give them a weapon and armor, can take some out, they defend kingdom)
 */

    public ushort m_WaterBearer_Count;
    public ushort m_WaterBearer_Max;

    public ushort m_Farmer_Count;
    public ushort m_Farmer_Max;

    public ushort m_Cook_Count;
    public ushort m_Cook_Max;

    public ushort m_Lumberjack_Count;
    public ushort m_Lumberjack_Max;

    public ushort m_Miner_Count;
    public ushort m_Miner_Max;

    public ushort m_Soldier_Count;
    public ushort m_Soldier_Max;

    public bool m_ArtisanAlive;
    public bool m_BlacksmithAlive;
    public bool m_BuilderAlive;
    public bool m_MageAlive;



}
