using UnityEngine;

[CreateAssetMenu(fileName = "Building_SO", menuName = "BuildingInfo")]
public class Building_SO : ScriptableObject
{
    public string buildingName;
    public string buildingDesc;
    public int buildingNumber;

    public string[] resources;
    public int[] costs;
}
