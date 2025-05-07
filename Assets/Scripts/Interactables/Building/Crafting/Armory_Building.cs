using UnityEngine;

public class Armory_Building : Crafting_Building
{
    #region Armory
    [SerializeField] private Weapon_SO[] weapons;
    [SerializeField] private bool[] isCrafted;

    private new void Start()
    {
        base.Start();
        craftingMenu = ui.armoryCraftingMenu;
    }

    #endregion
}
