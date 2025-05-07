using UnityEngine;

public class MouseSelection2 : MonoBehaviour
{
    [SerializeField] private LayerMask NPCMask;
    [SerializeField] private LayerMask buildingMask;
    [SerializeField] private LayerMask resourceMask;
    [SerializeField] private LayerMask interactableMask;

    [SerializeField] private GameObject hoveredObject;

    [SerializeField] private Texture2D mouseNPCHover;
    [SerializeField] private Texture2D mouseResourceHover;
    [SerializeField] private Texture2D mouseBase;
    [SerializeField] private Vector2 mouseHotspot;

    [SerializeField] private float interactableDistance;

    //private PlayerInteractions pi;

    private void Start()
    {
        
        //pi = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteractions>();
        //ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        interactableMask = NPCMask + resourceMask + buildingMask;
        Cursor.SetCursor(mouseBase, mouseHotspot, CursorMode.ForceSoftware);
    }

    private void Update()
    {
        DetectHoveredObject();

        if (Input.GetMouseButtonDown(1)) //right click
        {
            HandleRightClick();
        }
    }

    private void DetectHoveredObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 500, interactableMask))
        {
            if (hit.collider.gameObject == hoveredObject) return;
            hoveredObject = hit.collider.gameObject;
            HandleObjectHovered();
        }
        else if (null != hoveredObject)
            HandleNoHover();
    }

    private void HandleObjectHovered()
    {
        if (null == hoveredObject) return;

        if ((NPCMask.value & (1 << hoveredObject.layer)) != 0)
        {
            HandleNPCHover();
        }
        else if ((resourceMask.value & (1 << hoveredObject.layer)) != 0)
        {
            HandleResourceHover();
        }
        else if ((buildingMask.value & (1 << hoveredObject.layer)) != 0)
        {
            HandleBuildingHover();
        }
    }

    private void HandleNPCHover()
    {
        Cursor.SetCursor(mouseNPCHover, mouseHotspot, CursorMode.ForceSoftware);

        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        if (Vector3.Distance(playerPos, hoveredObject.transform.position) > interactableDistance) return;
        //make them outline?
    }

    private void HandleResourceHover()
    {
        Cursor.SetCursor(mouseResourceHover, mouseHotspot, CursorMode.ForceSoftware);

        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        if (Vector3.Distance(playerPos, hoveredObject.transform.position) > interactableDistance) return;
        //make it outline?
    }
    private void HandleBuildingHover()
    {
        Cursor.SetCursor(mouseNPCHover, mouseHotspot, CursorMode.ForceSoftware);

        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        if (Vector3.Distance(playerPos, hoveredObject.transform.position) > interactableDistance) return;
        //make it outline?
    }

    private void HandleRightClick()
    {
        if (null == hoveredObject) return;
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        if (Vector3.Distance(playerPos, hoveredObject.transform.position) > interactableDistance) return;

        if ((NPCMask.value & (1 << hoveredObject.layer)) != 0)
        {
            //open NPC managment UI
        }
        else if ((resourceMask.value & (1 << hoveredObject.layer)) != 0)
        {
            hoveredObject.GetComponent<Resource>().Interaction();
        }
        else if ((buildingMask.value & (1 << hoveredObject.layer)) != 0)
        {
            hoveredObject.GetComponent<Building>().Interaction();
        }
    }

    private void HandleNoHover()
    {
        Cursor.SetCursor(mouseBase, mouseHotspot, CursorMode.ForceSoftware);
        hoveredObject = null;
    }
}
