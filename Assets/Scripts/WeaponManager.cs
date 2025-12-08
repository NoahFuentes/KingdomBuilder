using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }
    public Weapon_SO[] weapons;

    [SerializeField] private Transform rightItemSpawn;
    [SerializeField] private Transform leftItemSpawn;

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

    public void CraftWeapon(string weaponName)
    {
        Weapon_SO weapon = GetWeaponDetailsByName(weaponName);

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
