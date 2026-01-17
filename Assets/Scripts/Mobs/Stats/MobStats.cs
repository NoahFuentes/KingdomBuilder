using UnityEngine;

public class MobStats : MonoBehaviour
{
    [Header("===============BASE STATS===============")]
    public MobSpawnerBase spawner; //used to keep track of spawning logic
    public Vector3 spawnPoint;
    public int maxHealth;
    public int currentHealth;
    public int healthRegenTime; //how much default state time until health resets


    [Header("NavMesh Stats")]
    public float baseMovementSpeed;
    public float sprintMovementSpeed;
    public float turningSpeed;
    public float acceleration;

    public float avoidanceDist;
    public float avoidanceHeight;


    [Header("Alertness Stats")]
    [SerializeField] private SphereCollider alertnessTrigger;
    public float alertRange;
    public float alertTime; //Time it takes for the mob to notice or not notice you. Ref alertState_mob


    [Header("Special Options")]
    public bool isStationary;
    public float wanderRadius;
    public float fleeDistance;
    public float maxDistFromSpawn;

    public bool isHostile;
    public float deagroRange;


    [Header("=================ATTACKING STATS================")]
    public DamageType damageType;
    public int damage;
    public float attackRate;
    public float attackDistance;
    [HideInInspector] public bool isAttacking;


    [Header("=================TAKING DAMAGE STATS================")]
    public float bluntResistance;
    public float slashResistance;
    public float pierceResistance;
    public float magicResistance;
    public float flinchDuration;


    public void HealHealth(int recoverAmt)
    {
        currentHealth = Mathf.Min(currentHealth + recoverAmt, maxHealth);
    }

    //UNITY FUNCTIONS

    private void Awake()
    {
        spawnPoint = transform.position;

        currentHealth = maxHealth;

        alertnessTrigger.isTrigger = true;
        alertnessTrigger.radius = alertRange;
    }

}
