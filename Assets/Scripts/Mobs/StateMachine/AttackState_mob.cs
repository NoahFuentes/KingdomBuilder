using UnityEngine;

public class AttackState_mob : IState
{
    private Transform target;
    private float lastAttackTime;

    private readonly MobBase mob;
    public AttackState_mob(MobBase mob)
    {
        this.mob = mob;
    }
    public void EnterState()
    {
        target = PlayerStats.Instance.transform;
        //stop movement of navmesh
        mob.agent.updatePosition = false;
        //face target
        mob.agent.SetDestination(target.position);
        //attack
        Attack();
    }

    public void TickState()
    {
        if (Time.time - lastAttackTime < mob.stats.attackRate) return;
        //keep attacking if player is in range
        if (Vector3.Distance(mob.transform.position, target.position) <= mob.stats.attackDistance) Attack();
        //go to reaction state if not in range
        else
        {
            mob.stateMachine.ChangeState(mob.reactionState);
            return;
        }
    }

    public void ExitState()
    {
        mob.agent.Warp(mob.transform.position);
        mob.agent.updatePosition = true;
        
    }

    private void Attack()
    {
        lastAttackTime = Time.time;
        //mob.animator.Play("attack"); //attack anim needs events to handle attack logic. trigger hitbox toggling
        Debug.Log("attacked");
    }

}
