using UnityEngine;

public class FleeState_mob : IState
{
    private Transform target;

    private readonly MobBase mob;
    public FleeState_mob(MobBase mob)
    {
        this.mob = mob;
    }
    public void EnterState()
    {
        Debug.Log("in flee state");
        target = PlayerStats.Instance.transform;
        //set agent to allow movement
        mob.agent.isStopped = false;
        mob.agent.updatePosition = true;
        //play run animation
        //mob.animator.Play("run");
        //set destination to away from target
        Vector3 fleeDir = (mob.transform.position - target.position).normalized;
        mob.agent.SetDestination(mob.transform.position + (fleeDir * mob.stats.fleeDistance));
    }

    public void TickState()
    {
        if (mob.agent.remainingDistance <= mob.agent.stoppingDistance) mob.stateMachine.ChangeState(mob.returnToSpawn);
    }

    public void ExitState()
    {
        
    }

}
