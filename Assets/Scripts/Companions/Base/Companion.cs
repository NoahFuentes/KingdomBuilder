using UnityEngine;
using UnityEngine.AI;
using TMPro;

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
    public TalkingState talking;

    [HideInInspector] public bool isTalking = false;

    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Animator animator;

    public Transform workPosition;
    public Transform homePosition;

    [SerializeField] protected GameObject talkingInterface; //Companion dialoug prefab
    [SerializeField] protected TextMeshProUGUI companionWords;

    public virtual void Talk()
    {
        companionWords.text = CompanionManager.Instance.greetings[Random.Range(0, CompanionManager.Instance.greetings.Length)];
        talkingInterface.SetActive(true);
        UIManager.Instance.StartCursorInteraction();
        isTalking = true;
        if (stateMachine.CurrentState != working)
            stateMachine.ChangeState(talking);
    }
    public virtual void EndTalk()
    {
        UIManager.Instance.EndCursorInteraction();
        UIManager.Instance.SetGOInactive(talkingInterface);
        isTalking = false;
        if (stateMachine.CurrentState != working)
        {
            if (GameClock.Instance.currentTimeOfDayMinutes >= GameClock.Instance.endOfWorkDayTime || GameClock.Instance.currentTimeOfDayMinutes < GameClock.Instance.startOfWorkDayTime)
                stateMachine.ChangeState(walkingHome);
            else
                stateMachine.ChangeState(idle);

        }
    }
    public virtual void PrimaryInteraction() {}
    public virtual void SecondInteraction() {}

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
        talking = new TalkingState(this);
    }
    public virtual void Start()
    {
        Debug.Log(info.companionName + " has spawned!");
        talkingInterface.SetActive(false);
        stateMachine.ChangeState(idle);
    }

    public virtual void Update()
    {
        stateMachine.Tick();
    }

}
