using UnityEngine;
using UnityEngine.UI;

public class Armory : BuildingBase
{
    public override void OnBuild()
    {
        base.OnBuild();
    }
    public override void OnDemolish()
    {
        base.OnDemolish();
    }
    public override void OnSelect()
    {
        base.OnSelect();
        UIManager.Instance.interactionButton.GetComponent<Button>().onClick.AddListener(() => UIManager.Instance.OpenArmoryCraftingMenu());
    }
}
