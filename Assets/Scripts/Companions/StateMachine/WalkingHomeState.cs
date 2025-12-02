using UnityEngine;

public class WalkingHomeState : BaseState
{
    private readonly Companion companion;

    public WalkingHomeState(Companion companion)
    {
        this.companion = companion;
    }

    public void EnterState()
    {
        companion.agent.isStopped = false;
        companion.agent.SetDestination(companion.homePosition.position);
        //companion.animator.Play("Walking");
        Debug.Log("Entered Walking Home.");
    }

    public void TickState()
    {
        if (Vector3.Distance(companion.transform.position, companion.homePosition.position) > 1f) return;
        companion.stateMachine.ChangeState(companion.atHome);
    }

    public void ExitState()
    {
        companion.agent.isStopped = true;
    }
}
