using UnityEngine;

public class ChaseState_mob : IState
{

    private Transform target;
    private readonly MobBase mob;
    public ChaseState_mob(MobBase mob)
    {
        this.mob = mob;

    }
    public void EnterState()
    {
        //set navmesh target to player pos
        target = PlayerStats.Instance.transform;
        //play running animation
       // mob.animator.Play("run");
        Debug.Log("in chase state");
    }

    public void TickState()
    {
        //if mob got to far from spawn pos, go to ReturnToSpawn state
        if (Vector3.Distance(mob.transform.position, mob.stats.spawnPoint) > mob.stats.maxDistFromSpawn)
            mob.stateMachine.ChangeState(mob.returnToSpawn);

        float distToTarget = Vector3.Distance(mob.transform.position, target.position);
        //if target is within chase range
        if (distToTarget < mob.stats.deagroRange)
            mob.agent.SetDestination(target.position);
        else mob.stateMachine.ChangeState(mob.returnToSpawn);
        //if target is within attack dist
        if (distToTarget <= mob.stats.attackDistance)
            mob.stateMachine.ChangeState(mob.attack);


    }

    public void ExitState()
    {
        //stop running animation
    }

}
