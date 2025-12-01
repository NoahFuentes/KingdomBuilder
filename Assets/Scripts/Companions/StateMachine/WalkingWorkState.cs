using UnityEngine;

public class WalkingWorkState : BaseState
{
    private readonly Companion companion;

    public WalkingWorkState(Companion companion)
    {
        this.companion = companion;
    }

    public void EnterState()
    {
        companion.agent.isStopped = false;
        companion.agent.SetDestination(companion.workPosition.position);
        companion.animator.Play("Walking");
    }

    public void TickState()
    {
        if (Vector3.Distance(companion.transform.position, companion.agent.destination) > companion.agent.stoppingDistance) return;
        companion.stateMachine.ChangeState(companion.working);
    }

    public void ExitState()
    {
        companion.agent.isStopped = true;
    }
}
