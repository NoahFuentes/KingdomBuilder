using UnityEngine;

public class AttackState_mob : IState
{
    private Transform target;
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
        mob.agent.SetDestination(PlayerStats.Instance.transform.position);
        //play attack animation
        //mob.animator.Play("attack"); //attack anim needs events to handle attack logic. trigger hitbox toggling and update isAttacking
    }

    public void TickState()
    {
        if (mob.stats.isAttacking) return;
        //keep attacking if player is in range
        if (Vector3.Distance(mob.transform.position, target.position) <= mob.stats.attackDistance)
            Debug.Log("Attacking");
        //mob.animator.Play("attack");
        //go to reaction state if not in range
        else mob.stateMachine.ChangeState(mob.reactionState);
    }

    public void ExitState()
    {
        mob.agent.Warp(mob.transform.position);
        mob.agent.updatePosition = true;
        
    }

}
