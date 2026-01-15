using UnityEngine;

public class AlertState_mob : IState
{
    private float alertStartTime;


    private readonly MobBase mob;
    public AlertState_mob(MobBase mob)
    {
        this.mob = mob;
    }
    public void EnterState()
    {
        //set time of noticing player
        alertStartTime = Time.time;
        //face player
        mob.agent.isStopped = false;
        mob.agent.updatePosition = false;
        mob.agent.SetDestination(PlayerStats.Instance.transform.position);
        //player alert animation
        //mob.animator.Play("alert");
        Debug.Log("in alert state");
    }

    public void TickState()
    {
        if (Time.time - alertStartTime < mob.stats.alertTime) return;
        //check if player is in range still after alertTime has passed
        if (Vector3.Distance(mob.transform.position, PlayerStats.Instance.transform.position) <= mob.stats.alertRange + 1f)
        {
            Debug.Log("player found!");
            mob.stateMachine.ChangeState(mob.reactionState);
            return;
        }
        else
        {
            Debug.Log("player left");
            mob.stateMachine.ChangeState(mob.defaultState); //player is gone
            return;
        }
    }

    public void ExitState()
    {
        mob.agent.Warp(mob.transform.position);
        mob.agent.updatePosition = true;
    }

}
