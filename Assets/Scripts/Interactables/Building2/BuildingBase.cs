using UnityEngine;
using UnityEngine.UI;

public class BuildingBase : MonoBehaviour
{
    [SerializeField] protected Building_SO buildingInfo;
    [SerializeField] private ushort buildingLevel;
    public Vector2Int originGridPos;

    public virtual void OnBuild() { }
    public virtual void OnDemolish()
    {
        UIManager.Instance.CloseBuildingInfoFooter();
        UIManager.Instance.EndCursorInteraction();
    }
    public virtual void OnSelect()
    {
        UIManager.Instance.interactionButton.GetComponent<Button>().onClick.RemoveAllListeners();
        BuildingManager.Instance.wsBuilding = gameObject;
        BuildingManager.Instance.SetBuildingToBuild(buildingInfo);

        UIManager.Instance.UpdateBuildingInfoFooter(buildingInfo, buildingLevel);
        UIManager.Instance.OpenBuildingInfoFooter();
    }
}
