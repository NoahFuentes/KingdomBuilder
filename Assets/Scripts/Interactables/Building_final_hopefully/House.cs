using UnityEngine;

public class House : Building
{
    public override void RestoreSelf()
    {
        base.RestoreSelf();
        KingdomStats.Instance.maxPopulation++;
    }
}
