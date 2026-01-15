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
        {
            //Debug.Log("chase: mob traveled too far from its spawn");
            mob.stateMachine.ChangeState(mob.returnToSpawn);
            return;
        }

        float distToTarget = Vector3.Distance(mob.transform.position, target.position);
        //if target is within chase range
        if (distToTarget < mob.stats.deagroRange)
        {
            mob.agent.SetDestination(target.position);
        }
        else
        {
            //Debug.Log("chase: player got too far away");
            mob.stateMachine.ChangeState(mob.returnToSpawn);
            return;
        }
            //if target is within attack dist
        if (distToTarget <= mob.stats.attackDistance)
        {
            //Debug.Log("chase: in attacking distance");
            mob.stateMachine.ChangeState(mob.attack);
            return;
        }


    }

    public void ExitState()
    {
        //stop running animation
    }

}
