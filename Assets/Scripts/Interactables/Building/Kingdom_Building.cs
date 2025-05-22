using UnityEngine;

public class Kingdom_Building : Building
{
    #region KingdomBuilding

    public void CallInventoryDepot()
    {
        PlayerInventory.Instance.depotToKingdom();
    }
    #endregion
}
