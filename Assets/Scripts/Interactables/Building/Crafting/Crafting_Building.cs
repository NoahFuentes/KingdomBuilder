using UnityEngine;

public class Crafting_Building : Building
{
    #region CraftingBuilding
    protected GameObject craftingMenu;

    public virtual void OpenCraftMenu()
    {
        CloseInteractionDisplay();
        craftingMenu.SetActive(true);
    }
    public virtual void CloseCraftMenu()
    {
        craftingMenu.SetActive(false);
    }

    protected new void Update()
    {
        base.Update();
        if (!craftingMenu.activeSelf) return;
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        if (Vector3.Distance(playerPos, transform.position) > interactionBreakDist)
        {
            CloseCraftMenu();
        }
    }
    #endregion
}
