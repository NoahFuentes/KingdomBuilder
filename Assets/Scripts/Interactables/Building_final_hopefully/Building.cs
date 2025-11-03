using UnityEngine;

public class Building : MonoBehaviour
{
    public Building_SO info;
    public bool isRestored = false;

    public void RestoreSelf()
    {
        if (!KingdomStats.Instance.CanAfford(info.resources, info.costs))
        {
            NotificationManager.Instance.Notify("Cannot afford " + info.buildingName, Color.red);
            return;
        }
        KingdomStats.Instance.RemoveResources(info.resources, info.costs);
        //play animation of restoration and swap meshes/prefabs
        isRestored = true;
    }
}
