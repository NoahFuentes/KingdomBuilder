using UnityEngine;

public class MenuBasedCompanion : Companion
{
    [SerializeField] private GameObject interactionMenu;
    public override void PrimaryInteraction()
    {
        talkingInterface.SetActive(false);
        interactionMenu.SetActive(true);
    }
    public void closeMenu()
    {
        interactionMenu.SetActive(false);
        talkingInterface.SetActive(true);
    }

    //UNITY FUNCTIONS

    public override void Start()
    {
        base.Start();
        interactionMenu.SetActive(false);
    }
}
