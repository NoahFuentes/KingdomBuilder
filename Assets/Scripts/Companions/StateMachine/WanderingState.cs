using UnityEngine;

public class WanderingState : BaseState
{
    private readonly Companion companion;

    public WanderingState(Companion companion)
    {
        this.companion = companion;
    }

    public void EnterState()
    {
        companion.agent.isStopped = false;
        //companion.agent.SetDestination(/*random location*/);
        companion.animator.Play("Walking");
    }

    public void TickState()
    {
        if (Vector3.Distance(companion.transform.position, companion.agent.destination) > companion.agent.stoppingDistance) return;
        companion.stateMachine.ChangeState(companion.idle);
    }

    public void ExitState()
    {
        companion.agent.isStopped = true;
    }
}
