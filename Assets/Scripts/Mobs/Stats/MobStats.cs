using UnityEngine;

public class MobStats : MonoBehaviour
{
    public short maxHealth;
    public float currentHealth;
    public float healthRegenTime;

    public float baseMovementSpeed;
    public float sprintMovementSpeed;

    public ushort damage;
    public string damageType;

    public float attackRate;
    public Vector3 attackDimensions;
    public float stoppingDistance;

}
