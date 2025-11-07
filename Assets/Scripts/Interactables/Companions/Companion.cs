using UnityEngine;

public class Companion : MonoBehaviour
{
    public Companion_SO info;

    [SerializeField] private Transform workPosition;
    [SerializeField] private Transform homePosition;

    [SerializeField] private GameObject interactionInterface; //Store, resource selection, etc. *Primary function of the companion*

    public virtual void Talk() //ENDCURSORINTERACTION ON THE END TALK BUTTON DOES NOT SET INTERACTING WITH UI TO FALSE... FIXME
    {
        Debug.Log("Talking with " + info.companionName);
        interactionInterface.SetActive(true);
        UIManager.Instance.StartCursorInteraction();
    }

    // UNITY FUNCTIONS
    public virtual void Start()
    {
        Debug.Log(info.companionName + " ::Start()");
        interactionInterface.SetActive(false);
    }

}
