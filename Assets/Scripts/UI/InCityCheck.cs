using UnityEngine;

public class InCityCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            UIManager.Instance.openKingdomOverlay();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            UIManager.Instance.openExploringOverlay();
    }
}
