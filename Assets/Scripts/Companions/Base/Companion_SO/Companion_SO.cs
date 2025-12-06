using UnityEngine;

[CreateAssetMenu(fileName = "Companion_SO", menuName = "CompanionInfo")]
public class Companion_SO : ScriptableObject
{
    public string occupation;
    public string companionName;
    public string buildingOfWork;

    public Sprite dialougImg;

    public string hint;
}
