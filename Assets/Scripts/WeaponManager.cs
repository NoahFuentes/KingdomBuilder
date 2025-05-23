using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }
    public Weapon_SO[] weapons;

    private void Awake()
    {
        Instance = this;
    }

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
