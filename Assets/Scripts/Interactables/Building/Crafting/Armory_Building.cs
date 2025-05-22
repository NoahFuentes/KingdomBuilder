using UnityEngine;

public class Armory_Building : Crafting_Building
{
    #region Armory
    [SerializeField] private Weapon_SO[] weapons;
    [SerializeField] private bool[] isCrafted;

    private void Start()
    {
        craftingMenu = UIManager.Instance.armoryCraftingMenu;
    }
    #endregion
}
