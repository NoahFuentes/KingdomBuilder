using UnityEngine;

public class NearToPlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject currentFocusedObject;

    //[SerializeField] private Transform checkTransform;
    [SerializeField] private float interactionDistance;
    [SerializeField] private LayerMask interactionMask;




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
