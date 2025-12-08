using UnityEngine;

public class WaterBearer : Companion
{
    [SerializeField] private float waitTime;
    [SerializeField] private int amountToGive;
    private float timeSenseLastWater;

    private void SetWaterValues()
    {
        timeSenseLastWater = Time.time;
    }

    //UNITY FUNCTIONS

    public override void Start()
    {
        base.Start();
        SetWaterValues();
    }

    public override void Update()
    {
        base.Update();
        if ((stateMachine.CurrentState == atHome) || (Time.time - timeSenseLastWater < waitTime) || stateMachine.CurrentState == walkingHome || isTalking) return;
        string[] res = { "water" };
        KingdomStats.Instance.AddResources(res, new int[] { amountToGive });
        timeSenseLastWater = Time.time;
    }
}
