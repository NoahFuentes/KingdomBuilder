using UnityEngine;
//This state is entered at the end of wandering. Companion then chooses to continue wandering or go back to work.
public class IdleState : BaseState
{
    private readonly Companion companion;

    private float timeToWait;
    private float waitStartTime;
    private float chanceToKeepWandering = 0.33f;

    public IdleState(Companion companion)
    {
        this.companion = companion;
    }

    public void EnterState()
    {
        companion.agent.isStopped = true;
        companion.animator.Play("Idle");
        timeToWait = Random.Range(3f, 8f);
        waitStartTime = Time.time;
    }

    public void TickState()
    {
        if (Time.time - waitStartTime < timeToWait) return;
        if (Random.Range(0f, 1f) <= chanceToKeepWandering)
            companion.stateMachine.ChangeState(companion.wandering);
        else
            companion.stateMachine.ChangeState(companion.walkingWork);
    }

    public void ExitState()
    {
        companion.agent.isStopped = false;
    }
}
