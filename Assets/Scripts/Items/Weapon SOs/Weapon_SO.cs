using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon")]
public class Weapon_SO : ScriptableObject
{
    public string weaponName;
    public string weaponDesc;
    public DamageType dmgType;

    public float attackRange; //only for weapons that need it
    public int damage;
    public float staminaCost;
    public float attackTime;

    public string[] resources;
    public int[] costs;

    public Sprite weaponIcon;
    public AnimatorOverrideController animController;
    public GameObject modelRight;
    public GameObject modelLeft;
}
