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
        Debug.Log(companion.info.occupation + " entered Wandering.");
        companion.agent.isStopped = false;
        companion.agent.SetDestination(CompanionManager.Instance.wanderPositions[Random.Range(0,CompanionManager.Instance.wanderPositions.Length)].position);
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
