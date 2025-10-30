/* This script manages all building placements and removals. buildingToBuild is the focus building
 * and is set externally through the setter function. Keeps an ongoing dictionary of placed buildings
 * and their grid positions used for building validity checking.
 */

/*
using UnityEngine;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    private Grid grid;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private List<Vector2Int> occupiedGridCells = new List<Vector2Int>();
    public bool isPlacing = false;

    private Building_SO buildingToBuild;

    private GameObject wsPlacer;
    public GameObject wsBuilding;

    [SerializeField] private float buildingInteractDistance;
    [SerializeField] private GameObject buildingGridVisual;
    private GameObject player;


    public void SetwsBuilding(GameObject newBuilding)
    {
        wsBuilding = newBuilding;
    }

    public void SetBuildingToBuild(Building_SO newBuilding)
    {
        buildingToBuild = newBuilding;
    }

    public void OpenPlacer()
    {
        if (buildingToBuild == null)
        {
            NotificationManager.Instance.Notify("No building selected", Color.red);
            return;
        }
        if (!KingdomStats.Instance.CanAfford(buildingToBuild.resources, buildingToBuild.costs))
        {
            NotificationManager.Instance.Notify("Cannot afford to build " + buildingToBuild.buildingName, Color.red);
            return;
        }
        if (BuldingAlreadyPlaced(buildingToBuild.buildingName))
        {
            NotificationManager.Instance.Notify(buildingToBuild.buildingName + " has already been built", Color.red);
            return;
        }
        wsPlacer = Instantiate(buildingToBuild.placer);
        isPlacing = true;
        buildingGridVisual.SetActive(isPlacing);
    }
    public void BuildBuilding()
    {
        Transform placementTrans = wsPlacer.transform;
        Vector3Int cell = grid.WorldToCell(placementTrans.position);
        Vector2Int gridPos = new Vector2Int(cell.x, cell.z);
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("KingdomBuilding").transform.position, placementTrans.position) > KingdomStats.Instance.m_KingdomRadius)
        {
            NotificationManager.Instance.Notify(buildingToBuild.buildingName + " is not within the kingdom border", Color.red);
            return;
        }
        if (CellIsOccupied(gridPos))
        {
            NotificationManager.Instance.Notify("Building space is already occupied", Color.red);
            return;
        }

        isPlacing = false;
        buildingGridVisual.SetActive(isPlacing);
        Destroy(wsPlacer);
        if (buildingToBuild == null)
        {
            NotificationManager.Instance.Notify("No building selected to place", Color.red);
            return;
        }
        if (!KingdomStats.Instance.CanAfford(buildingToBuild.resources, buildingToBuild.costs))
        {
            NotificationManager.Instance.Notify("Cannot afford to build " + buildingToBuild.buildingName, Color.red);
            return;
        } 
        
        GameObject wsBuilding = Instantiate(buildingToBuild.building, placementTrans.position, placementTrans.rotation);
        AddCellsToOccupiedList(gridPos);
        KingdomStats.Instance.RemoveResources(buildingToBuild.resources, buildingToBuild.costs);
        wsBuilding.GetComponent<BuildingBase>().originGridPos = gridPos;
        wsBuilding.GetComponent<BuildingBase>().OnBuild();
    }

    private bool BuldingAlreadyPlaced(string buildingName)
    {
        if (buildingName == "House") return false;
        //Debug.Log("Looking for building " + buildingName + "(Clone)");
        //Debug.Log((null != GameObject.Find(buildingName + "(Clone)") ? "FOUND" : "NOT FOUND"));
        return null != GameObject.Find(buildingName + "(Clone)");
    }

    private void AddCellsToOccupiedList(Vector2Int bottomLeftCell)
    {
        for(int x=0; x < buildingToBuild.gridSize.x; x++)
        {
            for (int y = 0; y < buildingToBuild.gridSize.y; y++)
            {
                occupiedGridCells.Add(new Vector2Int(bottomLeftCell.x+x, bottomLeftCell.y+y));
            }
        }
    }
    private void RemoveCellsToOccupiedList(Vector2Int bottomLeftCell)
    {
        for (int x = 0; x < buildingToBuild.gridSize.x; x++)
        {
            for (int y = 0; y < buildingToBuild.gridSize.y; y++)
            {
                occupiedGridCells.Remove(new Vector2Int(bottomLeftCell.x + x, bottomLeftCell.y + y));
            }
        }
    }
    private bool CellIsOccupied(Vector2Int bottomLeftCell)
    {
        for (int x = 0; x < buildingToBuild.gridSize.x; x++)
        {
            for (int y = 0; y < buildingToBuild.gridSize.y; y++)
            {
                if (occupiedGridCells.Contains(new Vector2Int(bottomLeftCell.x + x, bottomLeftCell.y + y))) return true;
            }
        }
        return false;
    }

    public void CancelBuildingPlacement()
    {
        isPlacing = false;
        Destroy(wsPlacer);
    }
    public void DemolishBuilding()
    {
        RemoveCellsToOccupiedList(wsBuilding.GetComponent<BuildingBase>().originGridPos);
        wsBuilding.GetComponent<BuildingBase>().OnDemolish();
        Destroy(wsBuilding);
        KingdomStats.Instance.AddResources(buildingToBuild.resources, buildingToBuild.costs);
    }


    //UNITY FUNCTIONS

    private void Awake()
    {
        Instance = this;
        grid = GetComponent<Grid>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void LateUpdate()
    {
        if (isPlacing && null != wsPlacer)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 500, groundMask))
            {
                Vector3Int cell = grid.WorldToCell(hit.point);
                Vector3 pos = grid.CellToWorld(cell);
                //Debug.Log("CELL: (" + cell.x + ", " + cell.z + ") POS: (" + pos.x + ", " + pos.z + ")");
                wsPlacer.transform.position = new Vector3(pos.x, wsPlacer.transform.localPosition.y, pos.z);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                wsPlacer.transform.Rotate(Vector3.up, 90f);
            }
            if (Input.GetMouseButtonDown(0))
            {
                BuildBuilding();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                CancelBuildingPlacement();
            }
        }
    }
    private void FixedUpdate()
    {
        if (wsBuilding == null) return;
        if(Vector3.Distance(player.transform.position, wsBuilding.transform.position) > buildingInteractDistance)
        {
           // UIManager.Instance.CloseBuildingInfoFooter();
          //  UIManager.Instance.EndCursorInteraction();
        }
    }
}
*/