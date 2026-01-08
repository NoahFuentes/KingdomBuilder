using UnityEngine;
using UnityEngine.AI;

public class WanderState_mob : IState
{
    private float timeEnteredWander;
    private float lastWanderTime;
    private float wanderDuration;

    private readonly MobBase mob;
    public WanderState_mob(MobBase mob)
    {
        this.mob = mob;
    }

    public void EnterState()
    {
        //set timeEnteredWander to time
        timeEnteredWander = Time.time;
        //free the movement of the mob
        mob.agent.isStopped = false;
        //set agent to walking speed
        mob.agent.speed = mob.stats.baseMovementSpeed;
    }

    public void TickState()
    {
        if (mob.agent.remainingDistance <= mob.agent.stoppingDistance) mob.animator.Play("idle");
        else mob.animator.Play("walk");
        //recover full health if left in default state for a spec time
        if (Time.time - timeEnteredWander >= mob.stats.healthRegenTime)
            mob.stats.HealHealth(mob.stats.maxHealth);
        //change the agent destination and choose a new wander duration if the prev duration has elapsed
        if(Time.time - lastWanderTime >= wanderDuration)
        {
            lastWanderTime = Time.time;
            wanderDuration = Random.Range(5f, 10f);
            mob.agent.SetDestination(RandomNavMeshPoint(mob.stats.spawnPoint, mob.stats.wanderRadius));
        }
    }

    public void ExitState()
    {
        //nothing
    }

    Vector3 RandomNavMeshPoint(Vector3 center, float radius)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector2 randomCircle = Random.insideUnitCircle * radius;
            Vector3 point = center + new Vector3(randomCircle.x, 0f, randomCircle.y);

            if (NavMesh.SamplePosition(point, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }

        return center; // fallback
    }

}
