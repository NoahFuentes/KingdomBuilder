using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon")]
public class Weapon_SO : ScriptableObject
{
    public string weaponName;
    public float damage;
    public string dmgType; //slash, pierce, blunt, magic
    public float attackRange; //mainly for ranged weapons
    public string weaponType; //SAS, Two-Handed, Bow, Staff

    public float dashDistance;
    public float dashSpeed;
    public float knockBackDist; //10-90 player back and target back

    public Sprite icon;
    public AnimatorOverrideController animController;
    public GameObject modelRight;
    public GameObject modelLeft;
}
