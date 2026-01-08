using UnityEngine;

public class DieState_mob : IState
{


    private readonly MobBase mob;
    public DieState_mob(MobBase mob)
    {
        this.mob = mob;
    }
    public void EnterState()
    {
        //stop all other animations.
        //stop movement
        //play death animation
        //report to spawner
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
