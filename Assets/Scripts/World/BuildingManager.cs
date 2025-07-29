/* This script manages all building placements and removals. buildingToBuild is the focus building
 * and is set externally through the setter function. Keeps an ongoing dictionary of placed buildings
 * and their grid positions used for building validity checking.
 */

using UnityEngine;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    private Grid grid;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private List<Vector2Int> occupiedGridCells = new List<Vector2Int>();
    private bool isPlacing = false;

    private Building_SO buildingToBuild;

    private GameObject wsPlacer;
    public GameObject wsBuilding;

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
            //TODO: notifier message
            Debug.Log("No building selected!");
            return;
        }
        if (!KingdomStats.Instance.CanAfford(buildingToBuild.resources, buildingToBuild.costs))
        {
            //TODO: notifier message
            Debug.Log("Cannot afford " + buildingToBuild.buildingName + "!");
            return;
        }
        wsPlacer = Instantiate(buildingToBuild.placer);
        isPlacing = true;
        UIManager.Instance.closeBuildMenu();
    }
    public void BuildBuilding()
    {
        Transform placementTrans = wsPlacer.transform;
        Vector3Int cell = grid.WorldToCell(placementTrans.position);
        Vector2Int gridPos = new Vector2Int(cell.x, cell.z);
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("KingdomBuilding").transform.position, placementTrans.position) > KingdomStats.Instance.m_KingdomRadius)
        {
            //TODO: notifier message
            Debug.Log(buildingToBuild.buildingName + " is too far from kingdom center!");
            return;
        }
        if (CellIsOccupied(gridPos))
        {
            //TODO: notifier message
            Debug.Log("Building collision detected!");
            return;
        }

        isPlacing = false;
        Destroy(wsPlacer);
        if (buildingToBuild == null)
        {
            //TODO: notifier message
            Debug.Log("No building selected!");
            return;
        }
        if (!KingdomStats.Instance.CanAfford(buildingToBuild.resources, buildingToBuild.costs))
        {
            //TODO: notifier message
            Debug.Log("Cannot afford " + buildingToBuild.buildingName + "!");
            return;
        } 
        
        GameObject wsBuilding = Instantiate(buildingToBuild.building, placementTrans.position, placementTrans.rotation);
        AddCellsToOccupiedList(gridPos);
        KingdomStats.Instance.RemoveResources(buildingToBuild.resources, buildingToBuild.costs);
        wsBuilding.GetComponent<BuildingBase>().originGridPos = gridPos;
        wsBuilding.GetComponent<BuildingBase>().OnBuild();
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
        //TODO: Remove cells from occupied list
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

    private void Update()
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
}
