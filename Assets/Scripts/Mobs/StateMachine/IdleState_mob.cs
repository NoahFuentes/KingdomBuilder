using UnityEngine;

public class IdleState_mob : IState
{
    private float timeEnteredIdle;


    private readonly MobBase mob;
    public IdleState_mob(MobBase mob)
    {
        this.mob = mob;
    }
    public void EnterState()
    {
        //set timeEnteredIdle
        timeEnteredIdle = Time.time;
        //set navmesh to not walk
        mob.agent.isStopped = true;
        //set animations to idle
        mob.animator.Play("Idle");
    }

    public void TickState()
    {
        //recover full health if left in default state for a spec time
        if (Time.time - timeEnteredIdle >= mob.stats.healthRegenTime)
            mob.stats.HealHealth(mob.stats.maxHealth);
    }

    public void ExitState()
    {
        //nothing
    }

}
