using UnityEngine;

public class WalkingWorkState : IState
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
        //companion.animator.Play("Walking");
        Debug.Log(companion.info.occupation + " entered Walking to Work.");
    }

    public void TickState()
    {
        if (Vector3.Distance(companion.transform.position, companion.workPosition.position) > 1f) return;
        companion.stateMachine.ChangeState(companion.working);
    }

    public void ExitState()
    {
        companion.agent.isStopped = true;
    }
}
