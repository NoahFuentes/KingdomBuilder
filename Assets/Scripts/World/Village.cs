using UnityEngine;
using System.Collections.Generic;

public class Village : MonoBehaviour
{
    [SerializeField] private string[] dailyResources;
    [SerializeField] private int[] dailyAmounts;

    public bool isSaved = false;
    [SerializeField] private List<GameObject> enemies;

    [SerializeField] private KeyCode testinput;

    //Call this from enemy death
    public void TrackEnemyDeath(GameObject enemy)
    {
        if (!enemies.Remove(enemy)) return;
        if (enemies.Count <= 0)
            isSaved = true;
    }

    public void ContributeToKingdom()
    {
        if (!isSaved) return;
        KingdomStats.Instance.AddResources(dailyResources, dailyAmounts);
    }


    private void Update()
    {
        if (Input.GetKeyDown(testinput)) isSaved = true; // TODO: remove this 
    }
}
