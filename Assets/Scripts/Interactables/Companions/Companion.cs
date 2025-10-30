using UnityEngine;

public class Companion : MonoBehaviour
{
    [SerializeField] private string companionName;
    public bool canInteract = true;
    public string nameOfBuilding;
    public virtual void Interact()
    {
        if (!canInteract) return;
        Debug.Log("Interacted with " + companionName);
        UIManager.Instance.interacting = true;
    }
}
