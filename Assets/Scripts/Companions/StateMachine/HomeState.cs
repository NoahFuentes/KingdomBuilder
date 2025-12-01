using UnityEngine;
using System.Collections;

public class HomeState : BaseState
{
    private readonly Companion companion;

    public HomeState(Companion companion)
    {
        this.companion = companion;
    }

    public void EnterState()
    {
        companion.agent.isStopped = true;
        companion.animator.Play("Interact");
        //fade companion out TODO
    }

    public void TickState()
    {
    }

    public void ExitState()
    {
        //fade companion in TODO
    }

    private IEnumerator WakeUp(float delay)
    {
        yield return new WaitForSeconds(delay);
        companion.stateMachine.ChangeState(companion.walkingWork);
    }
}
