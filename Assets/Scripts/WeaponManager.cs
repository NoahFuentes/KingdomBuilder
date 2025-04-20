using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon_SO[] weapons;

    public Weapon_SO GetWeaponDetailsByName(string name)
    {
        foreach(Weapon_SO weapon in weapons)
        {
            if (weapon.weaponName == name)
                return weapon;
        }
        Debug.Log("WeaponManger::GetWeaponByName: No weapon found by name: " + name);
        return null;
    }
}
