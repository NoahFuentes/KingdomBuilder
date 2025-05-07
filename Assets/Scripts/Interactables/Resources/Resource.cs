using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private string resName;
    public bool isMinable = false; //used to defer between axe and pickaxe


    public virtual void Interaction()
    {
        Debug.Log("Interacting with " + resName);
        if (isMinable) MineInteraction();
        else ChopInteraction();
    }

    private void MineInteraction()
    {
        Debug.Log(resName + " is minable");

    }
    private void ChopInteraction()
    {
        Debug.Log(resName + " is choppable");

    }
}
