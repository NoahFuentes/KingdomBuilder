using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LumberMill : BuildingBase
{
    [SerializeField] private float generationSeconds;
    private float lastGenTime;

    private bool isCuttingLumber;

    public override void OnBuild()
    {
        base.OnBuild();
    }
    public override void OnDemolish()
    {
        base.OnDemolish();
    }
    public override void OnSelect()
    {
        base.OnSelect();
        UIManager.Instance.interactionButton.GetComponent<Button>().onClick.AddListener(() => ToggleIsCuttingLumber());
    }

    private void ToggleIsCuttingLumber()
    {
        isCuttingLumber = !isCuttingLumber;
        UIManager.Instance.interactionButton.GetComponentInChildren<TextMeshProUGUI>().text = (isCuttingLumber ? "Chop Timber" : "Cut Lumber");
    }
    private void CutLumber()
    {
        if (!KingdomStats.Instance.CanAfford(new string[] { "timber" }, new int[] { 1 })) return;
        KingdomStats.Instance.RemoveResources(new string[] { "timber" }, new int[] {1});

        string[] res = {"lumber"};
        if(Random.Range(0f,1f) <= 0.05f) res = new string[] { "fine lumber" };
        KingdomStats.Instance.AddResources(res, new int[] {1});
    }
    private void ChopTimber()
    {
        string[] res = { "timber" };
        KingdomStats.Instance.AddResources(res, new int[] {1});
    }

    //UNITY FUNCTIONS

    private void Update()
    {
        if(Time.time - lastGenTime >= generationSeconds)
        {
            lastGenTime = Time.time;
            if (isCuttingLumber) CutLumber();
            else ChopTimber();
        }
    }
}
