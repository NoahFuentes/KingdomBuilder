using UnityEngine;
public class TalkingState : BaseState
{
    private readonly Companion companion;

    public TalkingState(Companion companion)
    {
        this.companion = companion;
    }

    public void EnterState()
    {
        companion.agent.isStopped = true;
        companion.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        //companion.animator.Play("Talking");
        Debug.Log(companion.info.occupation + " entered Talking.");
    }

    public void TickState()
    {
    }

    public void ExitState()
    {
        companion.agent.isStopped = false;
    }
}
