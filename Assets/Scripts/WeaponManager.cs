using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }

    public Weapon_SO[] weapons;
    public bool[] weaponCraftedStates;

    [SerializeField] private Transform rightItemSpawn;
    [SerializeField] private Transform leftItemSpawn;

    private void Awake()
    {
        Instance = this;
    }

    private Weapon_SO GetWeaponDetailsByName(string name)
    {
        foreach(Weapon_SO weapon in weapons)
        {
            if (weapon.weaponName == name)
                return weapon;
        }
        Debug.Log("WeaponManger::GetWeaponByName: No weapon found by name: " + name);
        return null;
    }
    private int GetWeaponIndexByName(string weaponName)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].weaponName == weaponName)
            {
                return i;
            }
        }
        Debug.Log("WeaponManger::GetWeaponIndexByName: No weapon found by name: " + name);
        return 0;
    }
    /*
    public void WeaponButtonInteract(string weaponName)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].weaponName == weaponName)
            {
                if (weaponCraftedStates[i])
                    EquipWeapon(weaponName);
                else
                    CraftWeapon(weaponName);
                return;
            }
        }
        Debug.Log("WeaponManager::WeaponButtonInteract: No weapon found with name: " + weaponName);
    }
    */
    public void CraftWeapon(string weaponName)
    {
        Weapon_SO weapon = GetWeaponDetailsByName(weaponName);
        if (!KingdomStats.Instance.CanAfford(weapon.resources, weapon.costs)) return;
        KingdomStats.Instance.RemoveResources(weapon.resources, weapon.costs);
        weaponCraftedStates[GetWeaponIndexByName(weaponName)] = true;
    }

    public void EquipWeapon(string weaponName)
    {
        Animator animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        Weapon_SO weapon = GetWeaponDetailsByName(weaponName);
        
        PlayerStats.Instance.m_CurrentWeapon = weapon;
        animator.runtimeAnimatorController = weapon.animController;
        if (rightItemSpawn.childCount != 0)
        {
            //remove old weapon model
            Destroy(rightItemSpawn.GetChild(0).gameObject);
        }
        if (leftItemSpawn.childCount != 0)
        {
            //remove old weapon model
            Destroy(leftItemSpawn.GetChild(0).gameObject);
        }
        //add new weapon model
        if (null != weapon.modelRight)
            AnimationController.Instance.playerWSWeaponR = Instantiate(weapon.modelRight, rightItemSpawn);
        if (null != weapon.modelLeft)
            AnimationController.Instance.playerWSWeaponL = Instantiate(weapon.modelLeft, leftItemSpawn);

        //TODO: update UI sprite
    }
}
