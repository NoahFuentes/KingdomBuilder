using UnityEngine;

public class ReturnToSpawnState_mob : IState
{


    private readonly MobBase mob;
    public ReturnToSpawnState_mob(MobBase mob)
    {
        this.mob = mob;
    }
    public void EnterState()
    {
        Debug.Log("in return to spawn state");
        //set navmesh target to spawn pos
        mob.agent.SetDestination(mob.stats.spawnPoint);
        //play running animation
       // mob.animator.Play("run");
    }

    public void TickState()
    {
        //if at spawn pos, go to default state (idle or wander)
        if(mob.agent.remainingDistance <= mob.agent.stoppingDistance) mob.stateMachine.ChangeState(mob.defaultState);
    }

    public void ExitState()
    {
        //stop running animation?
    }

}
