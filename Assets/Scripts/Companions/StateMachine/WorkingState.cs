using UnityEngine;

public class WorkingState : BaseState
{
    private readonly Companion companion;

    private float timeToWork;
    private float workStartTime;

    public WorkingState(Companion companion)
    {
        this.companion = companion;
    }

    public void EnterState()
    {
        companion.agent.isStopped = true;
        companion.transform.position = companion.workPosition.position;
        //companion.animator.Play("Working");
        timeToWork = Random.Range(60f, 180f);
        Debug.Log("Entered Working for " + timeToWork.ToString() + " seconds.");
        workStartTime = Time.time;
    }

    public void TickState()
    {
        if (Time.time - workStartTime < timeToWork) return;
        companion.stateMachine.ChangeState(companion.wandering);
    }

    public void ExitState()
    {
        companion.agent.isStopped = false;

    }
}
