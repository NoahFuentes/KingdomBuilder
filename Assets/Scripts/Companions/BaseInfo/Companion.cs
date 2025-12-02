using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Companion : MonoBehaviour
{
    public Companion_SO info;

    [HideInInspector] public StateMachine stateMachine;
    public IdleState idle;
    public WanderingState wandering;
    public WorkingState working;
    public HomeState atHome;
    public WalkingHomeState walkingHome;
    public WalkingWorkState walkingWork;

    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator animator;

    public Transform workPosition;
    public Transform homePosition;

    [SerializeField] protected GameObject interactionInterface; //Store, resource selection, etc. *Primary function of the companion*

    public virtual void Talk() //ENDCURSORINTERACTION ON THE END TALK BUTTON DOES NOT SET INTERACTING WITH UI TO FALSE... FIXME
    {
        Debug.Log("Talking with " + info.companionName);
        interactionInterface.SetActive(true);
        UIManager.Instance.StartCursorInteraction();
    }


    // UNITY FUNCTIONS

    public virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        stateMachine = new StateMachine();
        idle = new IdleState(this);
        wandering = new WanderingState(this);
        working = new WorkingState(this);
        atHome = new HomeState(this);
        walkingHome = new WalkingHomeState(this);
        walkingWork = new WalkingWorkState(this);
    }
    public virtual void Start()
    {
        Debug.Log(info.companionName + " has spawned!");
        interactionInterface.SetActive(false);
        stateMachine.ChangeState(idle);
    }

    public virtual void Update()
    {
        stateMachine.Tick();
    }

}
