using UnityEngine;

public class ChaseState_mob : BaseState
{


    private readonly MobBase mob;
    public ChaseState_mob(MobBase mob)
    {
        this.mob = mob;
    }
    public void EnterState()
    {
        //set navmesh target to player pos
        //play running animation
    }

    public void TickState()
    {
        //if mob got to far from spawn pos, go to ReturnToSpawn state

        //if player is within chase range
        //keep navmesh target set to player pos
        //else go to ReturnToSpawn state

        //if player is within attack range, go to attack state

    }

    public void ExitState()
    {
        //stop running animation
    }

}
