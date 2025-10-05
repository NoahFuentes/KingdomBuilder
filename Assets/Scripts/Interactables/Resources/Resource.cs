using UnityEngine;
using StarterAssets;

public class Resource : MonoBehaviour
{
    [SerializeField] private string resName;
    public bool isMinable = false; //used to defer between axe and pickaxe

    [SerializeField] private string resToGive;
    [SerializeField] private int amtToGive;

    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    public int TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0)
            Harvest();
        return currentHealth;
    }
    public void Harvest()
    {
        PlayerInventory.Instance.addResource(resToGive, amtToGive);
        GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>().canJump = true;
        Destroy(gameObject); //TODO: Make this actually do something
    }

    public virtual void Interaction()
    {
        if (PlayerInteractions.Instance.resourceInteractable == this) return;
        PlayerInteractions.Instance.resourceInteractable = this;
        //set world model to pick or axe based on isMinable
    }
}
