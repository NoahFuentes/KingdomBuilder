using UnityEngine;

public class House : BuildingBase
{

    public override void OnBuild()
    {
        KingdomStats.Instance.kingdomPopulationCap++;
        base.OnBuild();
    }
    public override void OnDemolish()
    {
        KingdomStats.Instance.kingdomPopulationCap--;
        base.OnDemolish();
    }
    public override void OnSelect()
    {
        base.OnSelect();
    }
}
