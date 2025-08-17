using UnityEngine;
using UnityEngine.UI;

public class Kingdom : BuildingBase
{
    private SphereCollider kingdomBuildCheck;
    //[SerializeField] private GameObject buildRangeIndicator;

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
        UIManager.Instance.interactionButton.GetComponent<Button>().onClick.AddListener(() => PlayerInventory.Instance.depotToKingdom());
    }



    //UNITY FUNCTIONS

    private void Start()
    {
        kingdomBuildCheck = GetComponentInParent<SphereCollider>();

        kingdomBuildCheck.radius = KingdomStats.Instance.m_KingdomRadius;
    }
    
}
