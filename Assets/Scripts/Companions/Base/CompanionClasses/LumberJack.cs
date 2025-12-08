using UnityEngine;
using TMPro;

public class LumberJack : Companion
{
    [SerializeField] private float timberInterval;
    [SerializeField] private float lumberInterval;
    [SerializeField] private float fineLumberChance;
    private float currWaitTime;
    private float timeSenseLastCut;


    [SerializeField] private bool processingLumber = false;

    [SerializeField] private TextMeshProUGUI toggleBtnTxt;
    [SerializeField] private string processLumberTxt;
    [SerializeField] private string cutTimberTxt;
    public override void PrimaryInteraction() 
    {
        processingLumber = !processingLumber;
        SetCuttingValues();
    }
    public override void SecondInteraction() { }

    private void SetCuttingValues()
    {
        timeSenseLastCut = Time.time;
        if (processingLumber)
        {
            toggleBtnTxt.text = cutTimberTxt;
            currWaitTime = lumberInterval;
        }
        else
        {
            toggleBtnTxt.text = processLumberTxt;
            currWaitTime = timberInterval;
        }
    }

    //UNITY FUNCTIONS

    public override void Start()
    {
        base.Start();
        SetCuttingValues();
    }

    public override void Update()
    {
        base.Update();
        if ((stateMachine.CurrentState == atHome) || (Time.time - timeSenseLastCut < currWaitTime) || stateMachine.CurrentState == walkingHome || isTalking) return;
        if (processingLumber)
        {
            if (!KingdomStats.Instance.CanAfford(new string[] { "timber" }, new int[] { 2 })) return;
            KingdomStats.Instance.RemoveResources(new string[] { "timber" }, new int[] { 2 });

            string[] res = { "lumber" };
            if (Random.Range(0f, 1f) <= fineLumberChance) res = new string[] { "fine lumber" };
            KingdomStats.Instance.AddResources(res, new int[] { 1 });
        }
        else
        {
            string[] res = { "timber" };
            KingdomStats.Instance.AddResources(res, new int[] { 1 });
        }

        timeSenseLastCut = Time.time;
    }
}
