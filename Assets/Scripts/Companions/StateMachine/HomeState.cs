using UnityEngine;
using System.Collections;

public class HomeState : IState
{
    private readonly Companion companion;

    public HomeState(Companion companion)
    {
        this.companion = companion;
    }

    public void EnterState()
    {
        companion.agent.isStopped = true;
        //companion.animator.Play("Interact"); // interact animation fades companion out
        Debug.Log(companion.info.occupation + " entered Home.");
    }

    public void TickState()
    {
    }

    public void ExitState()
    {
        //fade companion in TODO
    }
}
