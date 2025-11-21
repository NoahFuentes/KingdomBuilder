using UnityEngine;

public class InCityCheck : MonoBehaviour
{
    private int playerIsIn =0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsIn++;
            UIManager.Instance.openKingdomOverlay();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsIn--;
            if(0==playerIsIn)
                UIManager.Instance.openExploringOverlay();
        }
    }
}
