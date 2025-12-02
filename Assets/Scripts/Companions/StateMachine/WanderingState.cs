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
        Debug.Log("Entered Wandering.");
        companion.agent.isStopped = false;
        //companion.agent.SetDestination(/*random location*/);
        //companion.animator.Play("Walking");
    }

    public void TickState()
    {
        if (Vector3.Distance(companion.transform.position, companion.agent.destination) > 1f) return;
        companion.stateMachine.ChangeState(companion.idle);
    }

    public void ExitState()
    {
        companion.agent.isStopped = true;
    }
}
