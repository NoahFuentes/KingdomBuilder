using UnityEngine;

public class MobStats : MonoBehaviour
{
    public short maxHealth;
    public short currentHealth;
    public float healthRegenTime;

    public float baseMovementSpeed;
    public float sprintMovementSpeed;

    public Collider attackHitBox;
    public ushort damage;
    public string damageType;

    public float attackRate;
    public float attackRange;

    public string[] weaknessTypes;
    public string[] resistanceTypes;
}
