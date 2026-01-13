using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MobStats))]
public class MobBase : MonoBehaviour, IHurtBox
{
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator animator;
    [HideInInspector] public MobStats stats;

    [HideInInspector] public StateMachine stateMachine;
    [HideInInspector] public IState defaultState; //usually idle or wandering
    [HideInInspector] public AlertState_mob alert;
    [HideInInspector] public IState reactionState; //chase or flee
    [HideInInspector] public WasHitState_mob hit;
    [HideInInspector] public AttackState_mob attack;
    [HideInInspector] public DieState_mob die;
    [HideInInspector] public ReturnToSpawnState_mob returnToSpawn;


    public void TakeHit(int damage, DamageType damageType)
    {
        stats.currentHealth -= damage;
        if (stats.currentHealth <= 0)
            stateMachine.ChangeState(die);
        else
            stateMachine.ChangeState(hit);
    }

    //UNITY FUNCTIONS

    public virtual void Awake()
    {
        stats = GetComponent<MobStats>();

        agent = GetComponent<NavMeshAgent>();
        agent.speed = stats.baseMovementSpeed;
        agent.angularSpeed = stats.turningSpeed;
        agent.acceleration = stats.acceleration;
        agent.radius = stats.avoidanceDist;
        agent.height = stats.avoidanceHeight;

        animator = GetComponent<Animator>();

        stateMachine = new StateMachine();
        if(stats.isStationary)
            defaultState = new IdleState_mob(this);
        else
            defaultState = new WanderState_mob(this);

        if (stats.isHostile)
            reactionState = new ChaseState_mob(this);
        else
            reactionState = new FleeState_mob(this);

        alert = new AlertState_mob(this);
        hit = new WasHitState_mob(this);
        attack = new AttackState_mob(this);
        die = new DieState_mob(this);
        returnToSpawn = new ReturnToSpawnState_mob(this);

    }
    public virtual void Start()
    {
        stateMachine.ChangeState(defaultState);
    }
    private void Update()
    {
        stateMachine.Tick();
    }

}
