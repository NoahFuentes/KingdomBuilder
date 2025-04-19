using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon[] weapons;

    public Weapon_SO GetWeaponDetailsByName(string name)
    {
        foreach(Weapon weapon in weapons)
        {
            if (weapon.details.weaponName == name)
                return weapon.details;
        }
        Debug.Log("WeaponManger::GetWeaponByName: No weapon found by name: " + name);
        return null;
    }
}
