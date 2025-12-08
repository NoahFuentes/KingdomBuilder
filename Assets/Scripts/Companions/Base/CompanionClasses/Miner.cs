using UnityEngine;
using TMPro;

public class Miner : Companion
{

    [SerializeField] private string[] mineSelections;
    private int currentMineSelection = 0;
    [SerializeField] private float[] waitTimes;
    private float currWaitTime;
    private float timeSenseLastMine;

    [SerializeField] private string baseInteractionString;
    [SerializeField] private TextMeshProUGUI toggleBtnTxt;
    public override void PrimaryInteraction()
    {
        currentMineSelection++;
        if(currentMineSelection >= mineSelections.Length) currentMineSelection = 0;
        SetMiningValues();
    }
    public override void SecondInteraction() { }
    private string NextSelection()
    {
        int nextIndex = currentMineSelection + 1;
        if(nextIndex >= mineSelections.Length) nextIndex = 0;
        return mineSelections[nextIndex];
    }

    private void SetMiningValues()
    {
        timeSenseLastMine = Time.time;
        toggleBtnTxt.text = baseInteractionString + " " + NextSelection() + ".";
        currWaitTime = waitTimes[currentMineSelection];
    }

    //UNITY FUNCTIONS

    public override void Start()
    {
        base.Start();
        SetMiningValues();
    }

    public override void Update()
    {
        base.Update();
        if ((stateMachine.CurrentState == atHome) || (Time.time - timeSenseLastMine < currWaitTime) || stateMachine.CurrentState == walkingHome || isTalking) return;
        string[] res = { mineSelections[currentMineSelection] };
        KingdomStats.Instance.AddResources(res, new int[] { 1 });
        timeSenseLastMine = Time.time;
    }
}
