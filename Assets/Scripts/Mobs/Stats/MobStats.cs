using UnityEngine;

public class MobStats : MonoBehaviour
{
    [Header("Base Mob Stats")]
    public int maxHealth;
    public int currentHealth;
    public int healthRegenTime; //how much default state time until health resets

    public float baseMovementSpeed;
    public float sprintMovementSpeed;

    public float alertRange;
    public float alertTime; //Time it takes for the mob to notice or not notice you. Ref alertState_mob

    public bool isStationary;
    public float wanderRadius;

    public bool isHostile;

    public Vector3 spawnPoint;

    [Header("Attacking Stats")]
    public DamageType damageType;
    public int damage;

    public float attackDistance;




    public void HealHealth(int recoverAmt)
    {
        currentHealth = Mathf.Min(currentHealth + recoverAmt, maxHealth);
    }

}
