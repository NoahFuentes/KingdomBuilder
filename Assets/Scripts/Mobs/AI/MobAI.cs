using UnityEngine;
using UnityEngine.AI;

public class MobAI : MonoBehaviour //Master class for all Mob AIs, interactions and movement
{
    [SerializeField] protected float noticeRange;
    protected Collider[] targets;
    [SerializeField] protected LayerMask targetMask;

    [SerializeField] protected float wanderRange;
    [SerializeField] protected float wanderTime;
    protected float lastWanderTime;

    public MobSpawner spawner;
    public ushort spawnerIndex;
    
    protected MobStats stats;
    protected NavMeshAgent agent;
    protected Animator animator;


    protected void Start()
    {
        stats = GetComponent<MobStats>();

        agent = GetComponent<NavMeshAgent>();
        agent.speed = stats.baseMovementSpeed;
        agent.stoppingDistance = stats.stoppingDistance;

        animator = GetComponent<Animator>();
    }

    public virtual void TakeDamage(float dmg)
    {
        stats.currentHealth -= dmg;
        Debug.Log(gameObject.name + " took " + dmg + " damage: " + (stats.currentHealth + dmg) + " -> " + stats.currentHealth);
        if (stats.currentHealth <= 0)
        {
            Die();
            return;
        }
        //knockback?
    }

    protected void RestoreHealth(short healAmt)
    {
        if (stats.currentHealth + healAmt <= stats.maxHealth)
            stats.currentHealth += healAmt;
        else
            stats.currentHealth = stats.maxHealth;
    }

    protected void Die()
    {
        spawner.spawnCounts[spawnerIndex]--;
        Destroy(gameObject);
    }
}
