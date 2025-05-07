using UnityEngine;

public class Crafting_Building : Building
{
    #region CraftingBuilding
    [SerializeField] protected GameObject craftingMenu;

    public void OpenCraftMenu()
    {
        craftingMenu.SetActive(true);
    }
    public void CloseCraftMenu()
    {
        craftingMenu.SetActive(false);
    }

    private void Update()
    {
        if (!craftingMenu.activeSelf) return;
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        if (Vector3.Distance(playerPos, transform.position) > interactionBreakDist)
        {
            CloseCraftMenu();
        }
    }
    #endregion
}
