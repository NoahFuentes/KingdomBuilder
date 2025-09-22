using UnityEngine;

public class NearToPlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject currentFocusedObject;
    private Transform playerTransform;

    //[SerializeField] private Transform checkTransform;
    [SerializeField] private float interactionDistance;
    private LayerMask interactionMask;

    [SerializeField] private LayerMask buildingMask;
    [SerializeField] private LayerMask resourceMask;


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
        //take shader off
        currentFocusedObject = null;
    }

    private void FocusOnNewFocusObject(GameObject objectToFocusOn)
    {
        //set shader
        currentFocusedObject = objectToFocusOn;
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
        CharacterMovement.Instance.navMoveToPosition(currentFocusedObject.GetComponent<CapsuleCollider>().ClosestPoint(playerTransform.position));
        currentFocusedObject.GetComponent<Resource>().Interaction();
    }
    private void HandleBuildingInteraction()
    {
        currentFocusedObject.GetComponentInParent<BuildingBase>().OnSelect();
    }

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
        if (interactablesInRange.Length < 1) {
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
        if(closestInteractable.gameObject != currentFocusedObject)
            ChangeFocusedObject(closestInteractable.gameObject);
    }

}
