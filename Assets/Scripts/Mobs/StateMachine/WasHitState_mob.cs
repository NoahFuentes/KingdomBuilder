using UnityEngine;

public class WasHitState_mob : IState
{
    public float damageToTake;

    private readonly MobBase mob;
    public WasHitState_mob(MobBase mob)
    {
        this.mob = mob;
    }
    public void EnterState()
    {
        //stop all other animations.
        //stop movement
        //play hit animation
        //reduce health by damage to take
    }

    public void TickState()
    {
        //nothing
    }

    public void ExitState()
    {
        //nothing
    }

}
