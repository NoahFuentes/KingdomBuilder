using UnityEngine;

public class NearToPlayerInteraction : MonoBehaviour
{
    [SerializeField] private Material focusShaderMat;

    [SerializeField] private GameObject currentFocusedObject;
    private Transform playerTransform;

    //[SerializeField] private Transform checkTransform;
    [SerializeField] private float interactionDistance;
    private LayerMask interactionMask;

    [SerializeField] private LayerMask buildingMask;
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

        if ((resourceMask.value & (1 << currentFocusedObject.layer)) != 0)
        {
            HandleResourceInteraction();
        }
        else if ((buildingMask.value & (1 << currentFocusedObject.layer)) != 0)
        {
            HandleBuildingInteraction();
        }
    }

    private void HandleResourceInteraction()
    {
        CharacterMovement.Instance.navMoveToPosition(currentFocusedObject.GetComponentInChildren<CapsuleCollider>().ClosestPoint(playerTransform.position));
        currentFocusedObject.GetComponent<Resource>().Interaction();
    }
    private void HandleBuildingInteraction()
    {
        currentFocusedObject.GetComponent<BuildingBase>().OnSelect();
    }


    //UNITY FUNCTIONS

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        interactionMask = buildingMask + resourceMask;
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
            else if ((buildingMask.value & (1 << closestInteractable.gameObject.layer)) != 0)
                ChangeFocusedObject(closestInteractable.gameObject);

        }
    }
}
