using UnityEngine;

public class WeaponsmithMenu : MonoBehaviour
{
    public void CallWMEquipWeapon(string weaponName)
    {
        WeaponManager.Instance.EquipWeapon(weaponName);
    }
    public void CallWMCraftWeapon(string weaponName)
    {
        WeaponManager.Instance.CraftWeapon(weaponName);
    }
}
