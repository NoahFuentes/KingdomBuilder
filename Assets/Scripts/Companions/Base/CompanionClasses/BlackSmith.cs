using UnityEngine;

public class BlackSmith : Companion
{
    [SerializeField] private float copperTime;
    [SerializeField] private int copperOreCostPerBar;
    [SerializeField] private float ironTime;
    [SerializeField] private int ironOreCostPerBar;
    [SerializeField] private float goldTime;
    [SerializeField] private int goldOreCostPerBar;
    private float timeSenseLastCopper;
    private float timeSenseLastIron;
    private float timeSenseLastGold;

    private void SetMetalValues()
    {
        timeSenseLastCopper = Time.time;
        timeSenseLastIron = Time.time;
        timeSenseLastGold = Time.time;
    }

    //UNITY FUNCTIONS

    public override void Start()
    {
        base.Start();
        SetMetalValues();
    }

    public override void Update()
    {
        base.Update();
        if (stateMachine.CurrentState == atHome || stateMachine.CurrentState == walkingHome || isTalking) return;
        if (Time.time - timeSenseLastCopper >= copperTime)
        {
            if (KingdomStats.Instance.CanAfford(new string[] { "copper ore" }, new int[] { copperOreCostPerBar }))
            {
                KingdomStats.Instance.RemoveResources(new string[] { "copper ore" }, new int[] { copperOreCostPerBar });
                string[] res = { "copper" };
                KingdomStats.Instance.AddResources(res, new int[] { 1 });
            }
            timeSenseLastCopper = Time.time;
        }
        if (Time.time - timeSenseLastIron >= ironTime)
        {
            if (KingdomStats.Instance.CanAfford(new string[] { "iron ore" }, new int[] { ironOreCostPerBar }))
            {
                KingdomStats.Instance.RemoveResources(new string[] { "iron ore" }, new int[] { ironOreCostPerBar });
                string[] res = { "iron" };
                KingdomStats.Instance.AddResources(res, new int[] { 1 });
            }
            timeSenseLastIron = Time.time;
        }
        if (Time.time - timeSenseLastGold >= goldTime)
        {
            if (KingdomStats.Instance.CanAfford(new string[] { "gold ore" }, new int[] { goldOreCostPerBar }))
            {
                KingdomStats.Instance.RemoveResources(new string[] { "gold ore" }, new int[] { goldOreCostPerBar });
                string[] res = { "gold" };
                KingdomStats.Instance.AddResources(res, new int[] { 1 });
            }
            timeSenseLastGold = Time.time;
        }

    }
}
