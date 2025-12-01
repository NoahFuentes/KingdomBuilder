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
        companion.animator.Play("Interact"); // interact animation fades companion out and calls schedule wake up
    }

    public void TickState()
    {
    }

    public void ExitState()
    {
        //fade companion in TODO
    }
}
