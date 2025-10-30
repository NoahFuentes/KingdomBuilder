using UnityEngine;

public class Companion : MonoBehaviour
{
    public bool canInteract = true;
    public string nameOfBuilding;
    public virtual void Interact()
    {
        if (!canInteract) return;

        UIManager.Instance.interactingWithNPC = true;
    }
}
