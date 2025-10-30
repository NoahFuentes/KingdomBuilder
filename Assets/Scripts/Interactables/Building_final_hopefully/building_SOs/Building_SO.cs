using UnityEngine;

[CreateAssetMenu(fileName = "Building_SO", menuName = "BuildingInfo")]
public class Building_SO : ScriptableObject
{
    public string buildingName;
    public string buildingDesc;
    //public Sprite buildingIcon;

    public string[] resources;
    public int[] costs;

    //public Vector2Int gridSize; //must be square... rectangles are too much work...

    //public bool interactable; //has an assotiated menu that can be opened
    //public string interactButtonString;
    //public bool upgradable;
    //public bool demolishable;

    //public GameObject placer;
    public GameObject restoredBuildingPrefab;
}
