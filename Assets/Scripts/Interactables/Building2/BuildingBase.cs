using UnityEngine;

public class BuildingBase : MonoBehaviour
{
    [SerializeField] private Building_SO buildingInfo;
    [SerializeField] private ushort buildingLevel;


    public virtual void OnSelect()
    {
        BuildingManager.Instance.wsBuilding = gameObject;
        BuildingManager.Instance.SetBuildingToBuild(buildingInfo);

        UIManager.Instance.UpdateBuildingInfoFooter(buildingInfo, buildingLevel);
        UIManager.Instance.OpenBuildingInfoFooter();
    }
    public virtual void OnBuild() { }
    public virtual void OnDemolish()
    {
        UIManager.Instance.CloseBuildingInfoFooter();
    }
}
