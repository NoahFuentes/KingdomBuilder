using UnityEngine;

public class House : Building
{
    [SerializeField] private Transform frontDoor;
    public override void RestoreSelf()
    {
        base.RestoreSelf();
        KingdomStats.Instance.maxPopulation++;
        CompanionManager.Instance.homePositions.Add(frontDoor);
    }
}
