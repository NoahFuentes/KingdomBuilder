using UnityEngine;

public class MouseSelection : MonoBehaviour
{
    [SerializeField] private Collider distCheckCol;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float distToPlayer;

    [SerializeField] private LayerMask NPCMask;
    [SerializeField] private LayerMask buildingMask;
    [SerializeField] private LayerMask resourceMask;
    [SerializeField] private LayerMask interactableMask;

    [SerializeField] private LayerMask groundMask;

    [SerializeField] private GameObject hoveredObject;

    [SerializeField] private Texture2D mouseNPCHover;
    [SerializeField] private Texture2D mouseResourceHover;
    [SerializeField] private Texture2D mouseBase;
    [SerializeField] private Vector2 mouseHotspot;

    [SerializeField] private float buildingInteractableDistance;
    [SerializeField] private float NPCInteractableDistance;
    [SerializeField] private float resourceInteractableDistance;

    //private PlayerInteractions pi;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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
            
            if (hit.collider.gameObject != hoveredObject)
                hoveredObject = hit.collider.gameObject;
            distToPlayer = Vector3.Distance(playerTransform.position, hoveredObject.transform.position);
            HandleObjectHovered();
        }
        else if (null != hoveredObject)
            HandleNoHover();
    }

    private void GetColliderOfHoveredObject()
    {
        if (null == hoveredObject) return;

        Collider[] colliders = hoveredObject.GetComponents<Collider>();
        foreach (Collider col in colliders)
        {
            if (col is MeshCollider) continue;
            distCheckCol = col;
            return;
        }
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
        if (distToPlayer > NPCInteractableDistance)
        {
            Cursor.SetCursor(mouseBase, mouseHotspot, CursorMode.ForceSoftware);
            return;
        }
        Cursor.SetCursor(mouseNPCHover, mouseHotspot, CursorMode.ForceSoftware);
        //make them outline?
    }

    private void HandleResourceHover()
    {
        if (distToPlayer > resourceInteractableDistance)
        {
            Cursor.SetCursor(mouseBase, mouseHotspot, CursorMode.ForceSoftware);
            return;
        }
        Cursor.SetCursor(mouseResourceHover, mouseHotspot, CursorMode.ForceSoftware);
        //make it outline?
    }
    private void HandleBuildingHover()
    {
        if (distToPlayer > buildingInteractableDistance)
        {
            Cursor.SetCursor(mouseBase, mouseHotspot, CursorMode.ForceSoftware);
            return;
        }
        Cursor.SetCursor(mouseNPCHover, mouseHotspot, CursorMode.ForceSoftware);
        //make it outline?
    }

    private void HandleRightClick()
    {
        if (null == hoveredObject) return;

        if ((NPCMask.value & (1 << hoveredObject.layer)) != 0)
        {
            HandleNPCRightClick();
        }
        else if ((resourceMask.value & (1 << hoveredObject.layer)) != 0)
        {
            HandleResourceRightClick();
            //hoveredObject.GetComponent<Resource>().Interaction();
        }
        else if ((buildingMask.value & (1 << hoveredObject.layer)) != 0)
        {
            HandleBuildingRightClick();
        }
    }

    private void HandleNPCRightClick()
    {
        if (distToPlayer > NPCInteractableDistance) return;
        //TODO: do something??
    }

    private void HandleResourceRightClick()
    {
        if (distToPlayer > resourceInteractableDistance) return;
        CharacterMovement.Instance.navMoveToPosition(hoveredObject.GetComponent<CapsuleCollider>().ClosestPoint(playerTransform.position));
        hoveredObject.GetComponent<Resource>().Interaction();
    }
    private void HandleBuildingRightClick()
    {
        if (distToPlayer > buildingInteractableDistance) return;
        hoveredObject.GetComponentInParent<BuildingBase>().OnSelect();
    }

    private void HandleNoHover()
    {
        Cursor.SetCursor(mouseBase, mouseHotspot, CursorMode.ForceSoftware);
        hoveredObject = null;
        distCheckCol = null;
    }
}
