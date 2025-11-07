using UnityEngine;
using System.Collections.Generic;

public class Village : MonoBehaviour
{
    [SerializeField] private bool hasCompanionToSave = false; //set this to false when the companion has been saved
    [SerializeField] private string companionToSave; //Lumber Jack, Farmer, etc

    [SerializeField] private string[] dailyResources;
    [SerializeField] private int[] dailyAmounts;

    [SerializeField] private bool isSaved = false;
    [SerializeField] private List<GameObject> enemies;

    [SerializeField] private KeyCode testinput;

    //Call this from enemy death
    public void TrackEnemyDeath(GameObject enemy)
    {
        if (!enemies.Remove(enemy)) return;
        if (enemies.Count <= 0)
            SetVillageFree();
    }

    public void SetVillageFree()
    {
        isSaved = true;
        if (hasCompanionToSave)
        {
            CompanionManager.Instance.SetCompanionAsSaved(companionToSave);
            hasCompanionToSave = false;
        }
    }

    public void ContributeToKingdom()
    {
        if (!isSaved) return;
        KingdomStats.Instance.AddResources(dailyResources, dailyAmounts);
    }

    private void Update()
    {
        if (Input.GetKeyDown(testinput)) SetVillageFree();
    }
}
