using UnityEngine;

public class LumberMill : BuildingBase
{
    [SerializeField] private float generationSeconds;
    private float lastGenTime;

    public override void OnBuild()
    {
        
    }
    public override void OnDemolish()
    {
        
    }


    private void Update()
    {
        if(Time.time - lastGenTime >= generationSeconds)
        {
            lastGenTime = Time.time;
            KingdomStats.Instance.AddResources(new string[] {"timber"}, new int[] {1});
        }
    }
}
