using UnityEngine;
using StarterAssets;

public class NearToPlayerInteraction : MonoBehaviour
{
    public static NearToPlayerInteraction Instance;

    [SerializeField] private Material focusShaderMat;

    public GameObject currentFocusedObject;
    private Transform playerTransform;

    //[SerializeField] private Transform checkTransform;
    [SerializeField] private float interactionDistance;
    private LayerMask interactionMask;

    [SerializeField] private LayerMask npcMask;
    [SerializeField] private LayerMask buildingRestorationMask;
    [SerializeField] private LayerMask resourceMask;

    private MeshRenderer focusedObjectRenderer;
    private Material[] focusedObjOriginalMats;

    private void ChangeFocusedObject(GameObject objectToFocusOn)
    {
        //if there is a current focus obj, clear it
        if (null != currentFocusedObject)
            ClearFocusedObject();

        //focus on the new obj
        FocusOnNewFocusObject(objectToFocusOn);
    }

    private void ClearFocusedObject()
    {
        if (null == currentFocusedObject) return;
        //take off focus shader
        focusedObjectRenderer.materials = focusedObjOriginalMats;

        currentFocusedObject = null;
    }

    private void FocusOnNewFocusObject(GameObject objectToFocusOn)
    {
        if (objectToFocusOn.TryGetComponent<Building>(out Building building))
            if (building.isRestored) return;
        currentFocusedObject = objectToFocusOn;
        focusedObjectRenderer = currentFocusedObject.GetComponentInChildren<MeshRenderer>();
        focusedObjOriginalMats = focusedObjectRenderer.materials;

        //set focus shader
        Material[] newMats = new Material[focusedObjOriginalMats.Length + 1];
        for (int i = 0; i < focusedObjOriginalMats.Length; i++)
            newMats[i] = focusedObjOriginalMats[i];

        // Add the outline as the last material
        newMats[newMats.Length - 1] = focusShaderMat;

        focusedObjectRenderer.materials = newMats;
    }

    private void InteractWithFocusedObject()
    {
        if (null == currentFocusedObject) return;
        if (currentFocusedObject.TryGetComponent<Resource>(out Resource res))
        {
            HandleResourceInteraction(res);
        }
        else if (currentFocusedObject.TryGetComponent<Companion>(out Companion companion))
        {
            HandleNPCInteraction(companion);
        }
        else if (currentFocusedObject.TryGetComponent<Building>(out Building building))
        {
            if (building.isRestored) return;
            HandleBuildingInteraction(building.info);
        }
            
    }

    private void HandleResourceInteraction(Resource res)
    {
        if (null == currentFocusedObject || null == res) return;
        Animator animator = GetComponentInParent<Animator>();
        transform.parent.LookAt(currentFocusedObject.transform);
        if (res.isMinable)
        {
            //mine
            animator.SetBool("isChopping", false);
            animator.SetBool("isMining", true);

        }
        else
        {
            //chop
            animator.SetBool("isMining", false);
            animator.SetBool("isChopping", true);

        }
        transform.parent.GetComponent<ThirdPersonController>().canJump = false;
        res.Interaction();
    } 
    private void HandleNPCInteraction(Companion companion)
    {
        if (null == currentFocusedObject || null == companion) return;
        companion.Interact();
    }
    private void HandleBuildingInteraction(Building_SO buildingInfo)
    {
        if (null == currentFocusedObject || null == buildingInfo) return;
        if (!KingdomStats.Instance.CanAfford(buildingInfo.resources, buildingInfo.costs))
        {
            NotificationManager.Instance.Notify("Cannot afford " + buildingInfo.buildingName, Color.red);
            return;
        }
        UIManager.Instance.PromptForRestoration(buildingInfo);
    }

    //UNITY FUNCTIONS

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        interactionMask = npcMask + resourceMask + buildingRestorationMask;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            InteractWithFocusedObject();
    }

    private void FixedUpdate()
    {
        //get all colliders with interactabel layer
        Collider[] interactablesInRange = Physics.OverlapSphere(transform.position, interactionDistance, interactionMask);
        if (interactablesInRange.Length < 1)
        {
            ClearFocusedObject();   //if no objects are found, clear the focus object
            return;
        }

        //find the closest one
        Collider closestInteractable = interactablesInRange[0];
        float closestInteractableDist = Vector3.Distance(transform.position, interactablesInRange[0].transform.position);
        foreach (Collider col in interactablesInRange)
        {
            float distToCol = Vector3.Distance(transform.position, col.transform.position);
            if (distToCol < closestInteractableDist)
            {
                closestInteractable = col;
                closestInteractableDist = distToCol;
            }
        }
        //if the closest is not the focus object, change to the closest
        if (closestInteractable.gameObject != currentFocusedObject)
        {
            if ((resourceMask.value & (1 << closestInteractable.gameObject.layer)) != 0)
                ChangeFocusedObject(closestInteractable.transform.parent.gameObject);
            else if ((npcMask.value & (1 << closestInteractable.gameObject.layer)) != 0)
                ChangeFocusedObject(closestInteractable.gameObject);
            else if ((buildingRestorationMask.value & (1 << closestInteractable.gameObject.layer)) != 0)
                ChangeFocusedObject(closestInteractable.transform.parent.gameObject);
            //ChangeFocusedObject(closestInteractable.transform.parent.gameObject);

        }
    }
}
