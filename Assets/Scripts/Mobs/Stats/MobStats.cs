using UnityEngine;

public class MobStats : MonoBehaviour
{
    public short maxHealth;
    public float currentHealth;
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

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        Debug.Log(gameObject.name + " took " + dmg + " damage: " + (currentHealth + dmg) + " -> " + currentHealth);
        if (currentHealth <= 0)
            KillMob();
    }
    public void KillMob()
    {
        Destroy(gameObject);
    }
}
