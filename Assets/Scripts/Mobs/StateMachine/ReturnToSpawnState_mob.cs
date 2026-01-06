using UnityEngine;

public class ReturnToSpawnState_mob : BaseState
{


    private readonly MobBase mob;
    public ReturnToSpawnState_mob(MobBase mob)
    {
        this.mob = mob;
    }
    public void EnterState()
    {
        //set navmesh target to spawn pos
        //play running animation
    }

    public void TickState()
    {
        //if at spawn pos, go to default state (idle or wander)
    }

    public void ExitState()
    {
        //stop running animation
    }

}
