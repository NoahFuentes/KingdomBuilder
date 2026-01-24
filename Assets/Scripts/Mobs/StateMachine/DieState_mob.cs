using UnityEngine;

public class DieState_mob : IState
{


    private readonly MobBase mob;
    public DieState_mob(MobBase mob)
    {
        this.mob = mob;
    }
    public void EnterState()
    {
        Debug.Log("in death state");
        //stop movement
        mob.agent.isStopped = true;
        mob.agent.updatePosition = false;
        //play death animation
        //mob.animator.Play("death");
        //report to spawner
        mob.stats.spawner.ReportMobDeath();
    }

    public void TickState()
    {
        //nothing for now
    }

    public void ExitState()
    {
        mob.agent.isStopped = false;
        mob.agent.updatePosition = true;
    }

}
