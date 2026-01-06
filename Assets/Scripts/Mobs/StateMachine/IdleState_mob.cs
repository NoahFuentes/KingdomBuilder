using UnityEngine;

public class IdleState_mob : BaseState
{


    private readonly MobBase mob;
    public IdleState_mob(MobBase mob)
    {
        this.mob = mob;
    }
    public void EnterState()
    {
        //set navmesh to not walk
        //set animations to idle
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
