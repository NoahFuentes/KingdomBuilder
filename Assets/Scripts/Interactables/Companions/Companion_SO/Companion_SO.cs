using UnityEngine;

[CreateAssetMenu(fileName = "Companion_SO", menuName = "CompanionInfo")]
public class Companion_SO : ScriptableObject
{
    public string occupation;
    public string companionName;
    public string buildingOfWork;
    //maybe keep a list of strings here for player dialog?
}
