using UnityEngine;

public class Farmer : Companion
{

    [SerializeField] private float foodTime;
    [SerializeField] private float clothTime;
    [SerializeField] private float leatherTime;
    private float timeSenseLastFood;
    private float timeSenseLastCloth;
    private float timeSenseLastLeather;

    private void SetFarmValues()
    {
        timeSenseLastFood = Time.time;
        timeSenseLastCloth = Time.time;
        timeSenseLastLeather = Time.time;
    }

    //UNITY FUNCTIONS

    public override void Start()
    {
        base.Start();
        SetFarmValues();
    }

    public override void Update()
    {
        base.Update();
        if (stateMachine.CurrentState == atHome || stateMachine.CurrentState == walkingHome || isTalking) return;
        if (Time.time - timeSenseLastFood >= foodTime)
        {
            string[] res = { "food" };
            KingdomStats.Instance.AddResources(res, new int[] { 1 });
            timeSenseLastFood = Time.time;
        }
        if (Time.time - timeSenseLastCloth >= clothTime)
        {
            string[] res = { "cloth" };
            KingdomStats.Instance.AddResources(res, new int[] { 1 });
            timeSenseLastCloth = Time.time;
        }
        if (Time.time - timeSenseLastLeather >= leatherTime)
        {
            string[] res = { "leather" };
            KingdomStats.Instance.AddResources(res, new int[] { 1 });
            timeSenseLastLeather = Time.time;
        }
    }
}
