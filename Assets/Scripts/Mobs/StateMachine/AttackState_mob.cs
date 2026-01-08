using UnityEngine;

public class AttackState_mob : IState
{


    private readonly MobBase mob;
    public AttackState_mob(MobBase mob)
    {
        this.mob = mob;
    }
    public void EnterState()
    {
        //stop movement of navmesh
        //face target
        //play attack animation
    }

    public void TickState()
    {
        //keep attacking if player is in range
        //go to reaction state if not in range
    }

    public void ExitState()
    {
        //nothing
    }

}
