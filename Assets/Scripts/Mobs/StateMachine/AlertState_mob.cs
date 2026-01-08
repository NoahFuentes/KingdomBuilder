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
        mob.transform.LookAt(PlayerStats.Instance.transform.position);
        //player alert animation
    }

    public void TickState()
    {
        if (Time.time - alertStartTime < mob.alertTime) return;
        //check if player is in range still after alertTime has passed
        if (Vector3.Distance(mob.transform.position, PlayerStats.Instance.transform.position) <= mob.alertRange)
            mob.stateMachine.ChangeState(mob.reactionState);
        else
            mob.stateMachine.ChangeState(mob.defaultState); //player is gone
    }

    public void ExitState()
    {
        //nothing
    }

}
