using UnityEngine;

public class Crafting_Building : Building
{
    #region CraftingBuilding
    [SerializeField] protected GameObject craftingMenu;

    protected UIManager ui;

    public virtual void OpenCraftMenu()
    {
        craftingMenu.SetActive(true);
        CloseInteractionDisplay();
    }
    public virtual void CloseCraftMenu()
    {
        craftingMenu.SetActive(false);
    }


    protected void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
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
