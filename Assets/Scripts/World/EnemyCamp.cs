using UnityEngine;
using System.Collections.Generic;

public class EnemyCamp : MonoBehaviour
{
    [SerializeField] private string companionToSave; //Lumber Jack, Farmer, etc

    [SerializeField] private List<GameObject> enemies;

    [SerializeField] private KeyCode testinput;

    //Call this from enemy death
    public void TrackEnemyDeath(GameObject enemy)
    {
        if (!enemies.Remove(enemy)) return;
        if (enemies.Count <= 0)
            CompanionManager.Instance.SetCompanionAsSaved(companionToSave);
    }

    private void Update()
    {
        if (Input.GetKeyDown(testinput)) CompanionManager.Instance.SetCompanionAsSaved(companionToSave);
    }
}
