using UnityEngine;
using UnityEngine.AI;

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
        fleeDir.y = 0;
        Vector3 rawDestination = mob.transform.position + (fleeDir * mob.stats.fleeDistance);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(rawDestination, out hit, 1000f, NavMesh.AllAreas))
        {
            // Snap only the Y to NavMesh, keep X/Z strict
            Vector3 finalPosition = new Vector3(rawDestination.x, hit.position.y, rawDestination.z);
            mob.agent.SetDestination(finalPosition);
        }
        else
        {
            Debug.LogWarning("No valid NavMesh Y found at this X/Z!");
        }
    }

    public void TickState()
    {
        if (mob.agent.remainingDistance <= mob.agent.stoppingDistance)
        {
            mob.stateMachine.ChangeState(mob.returnToSpawn);
            return;
        }
    }

    public void ExitState()
    {
        
    }

}
