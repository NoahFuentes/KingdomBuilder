using UnityEngine;

public class WasHitState_mob : IState
{
    //DAMAGING OF THE MOB IS DONE IN THE HURTBOX.TAKEDAMAGE() CALL **********NOT HERE!!!*********

    private float hitStartTime;

    private readonly MobBase mob;
    public WasHitState_mob(MobBase mob)
    {
        this.mob = mob;
    }
    public void EnterState()
    {
        Debug.Log("in wasHit state");
        //stop movement
        mob.agent.isStopped = true;
        mob.agent.updatePosition = false;
        //play hit animation
        //mob.animator.Play("hit");
        hitStartTime = Time.time;
    }

    public void TickState()
    {
        if (Time.time - hitStartTime >= mob.stats.flinchDuration)
        {
            mob.stateMachine.ChangeState(mob.reactionState);
            return;
        }
    }

    public void ExitState()
    {
        mob.agent.isStopped = false;
        mob.agent.updatePosition = true;
    }

}
