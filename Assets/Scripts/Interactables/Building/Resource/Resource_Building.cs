using UnityEngine;

public class Resource_Building : Building
{
    [SerializeField] private string resourcetype;
    [SerializeField] private int[] amountsForLevel;
    [SerializeField] private float resourceGivePeriod;
    private float lastResourceGain;
    private KingdomStats ks;

    protected void Start()
    {
        lastResourceGain = Time.time;
        ks = GameObject.FindGameObjectWithTag("KingdomManager").GetComponent<KingdomStats>();
    }

    protected new void Update()
    {
        base.Update();
        if (!isOccupied) return;
        if(Time.time - lastResourceGain > resourceGivePeriod)
        {
            Debug.Log("Giving " + amountsForLevel[buildingLevel] + " " + resourcetype);
            string[] resArr = {resourcetype};
            int[] amtArr = {amountsForLevel[buildingLevel]};
            ks.AddResources(resArr, amtArr);
            lastResourceGain = Time.time;
        }
    }
}
