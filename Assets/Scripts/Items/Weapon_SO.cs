using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon")]
public class Weapon_SO : ScriptableObject
{
    public string weaponName;
    public float damage;
    public string dmgType;
    public float attackRange;
    public string weaponType;
    public bool hasTargetedAttacks;

    public Sprite icon;
    public AnimatorOverrideController animController;
    public GameObject modelRight;
    public GameObject modelLeft;
}
