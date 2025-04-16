using UnityEngine;

public class KingdomStats : MonoBehaviour
{
    //Location definition
    public ushort m_KingdomLevel; // (Campsite, Town Center, Grand Courtyard, Great Hall, High Keep)
    public float m_KingdomRadius;


    //Resources (Currently: Food, water, wood, textiles, stone, iron, gold, crystal, and black crystal) 
    public uint m_MaxFoodAmount;
    public uint m_CurrentFoodAmount;
    public uint m_FoodConsumptionAmount;

    public uint m_MaxWaterAmount;
    public uint m_CurrentWaterAmount;
    public uint m_WaterConsumptionAmount;

    public uint m_MaxWoodAmount;
    public uint m_CurrentWoodAmount;
    public uint m_WoodConsumptionAmount;

    public uint m_MaxTextileAmount;
    public uint m_CurrentTextileAmount;

    public uint m_MaxStoneAmount;
    public uint m_CurrentStoneAmount;

    public uint m_MaxIronAmount;
    public uint m_CurrentIronAmount;

    public uint m_MaxGoldAmount;
    public uint m_CurrentGoldAmount;

    public uint m_MaxCrystalAmount;
    public uint m_CurrentCrystalAmount;

    public uint m_MaxBlackCrystalAmount;
    public uint m_CurrentBlackCrystalAmount;

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
