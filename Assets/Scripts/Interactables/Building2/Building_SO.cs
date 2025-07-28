using UnityEngine;

[CreateAssetMenu(fileName = "Building_SO", menuName = "BuildingInfo")]
public class Building_SO : ScriptableObject
{
    public string buildingName;
    public string buildingDesc;
    public Sprite buildingIcon;

    public string[] resources;
    public int[] costs;

    public Vector2Int gridSize;

    public GameObject placer;
    public GameObject building;
}
