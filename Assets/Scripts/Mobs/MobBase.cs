using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MobStatsBase))]
public class MobBase : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    [HideInInspector] public StateMachine stateMachine;
    [HideInInspector] public BaseState defaultState; //usually idle or wandering
    [HideInInspector] public AlertState_mob alert;
    [HideInInspector] public BaseState reactionState; //chase or flee
    [HideInInspector] public WasHitState_mob hit;
    [HideInInspector] public DieState_mob die;
    [HideInInspector] public ReturnToSpawnState_mob returnToSpawn;

    [SerializeField] private bool isStationary;
    [SerializeField] private bool isHostile;


    public float alertRange;
    public float alertTime;



    //UNITY FUNCTIONS

    public virtual void Awake()
    {
       agent = GetComponent<NavMeshAgent>();
       animator = GetComponent<Animator>();

        stateMachine = new StateMachine();
        if(isStationary)
            defaultState = new IdleState_mob(this);
        else
            defaultState = new WanderState_mob(this);

        if (isHostile)
            reactionState = new ChaseState_mob(this);
        else
            reactionState = new FleeState_mob(this);

        alert = new AlertState_mob(this);
        hit = new WasHitState_mob(this);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            stateMachine.ChangeState(alert);
    }
}
