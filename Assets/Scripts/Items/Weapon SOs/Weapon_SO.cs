using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon")]
public class Weapon_SO : ScriptableObject
{
    public string weaponName;
    public string weaponDesc;
    public string dmgType; //melee, ranged, magic
    public float attackRange; //mainly for ranged weapons

    public float damage;
    public float staminaCost;
    public float attackTime;
    public float knockBackDist;

    public string[] resources;
    public int[] costs;

    public Sprite weaponIcon;
    public AnimatorOverrideController animController;
    public GameObject modelRight;
    public GameObject modelLeft;
    public Vector3 attackDimensions;
}
