using UnityEngine;

public class Building : MonoBehaviour
{
    public Building_SO info;
    public GameObject brokenBuilding;
    public GameObject restoredBuilding;

    public bool isRestored = false;

    public virtual void RestoreSelf()
    {
        if (!KingdomStats.Instance.CanAfford(info.resources, info.costs))
        {
            NotificationManager.Instance.Notify("Cannot afford " + info.buildingName, Color.red);
            return;
        }
        KingdomStats.Instance.RemoveResources(info.resources, info.costs);
        //play animation of restoration and swap meshes/prefabs
        brokenBuilding.SetActive(false);
        restoredBuilding.SetActive(true);
        isRestored = true;
        //child classes will set kingdom stat restoration bools for themselves
    }
}
