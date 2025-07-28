using UnityEngine;

public class House : BuildingBase
{

    public override void OnBuild()
    {
        KingdomStats.Instance.kingdomPopulationCap++;
    }
    public override void OnDemolish()
    {
        KingdomStats.Instance.kingdomPopulationCap--;
        base.OnDemolish();
    }
}
