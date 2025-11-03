using UnityEngine;

public class Companion : MonoBehaviour
{
    [SerializeField] private string occupation;
    [SerializeField] private string companionName;
    public bool canInteract = true;
    [SerializeField] private string nameOfBuilding;

    [SerializeField] private Vector3 workPosition;
    [SerializeField] private Vector3 homePosition;

    [SerializeField] private GameObject interactionInterface; //Store, resource selection, etc. *Primary function of the companion*
    
    public virtual void Interact()
    {
        if (!canInteract) return;
        Debug.Log("Interacted with " + companionName);
        UIManager.Instance.interacting = true;
        UIManager.Instance.StartCursorInteraction();
        interactionInterface.SetActive(true);
    }

    // UNITY FUNCTIONS
    public virtual void Start()
    {
        Debug.Log(companionName + " ::Start()");
        interactionInterface.SetActive(false);
    }

}
