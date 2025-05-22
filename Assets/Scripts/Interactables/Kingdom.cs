using UnityEngine;

public class Kingdom : MonoBehaviour
{
    private SphereCollider kingdomBuildCheck;

    [SerializeField] private GameObject buildRangeIndicator;

    private void Start()
    {
        kingdomBuildCheck = GetComponent<SphereCollider>();

        kingdomBuildCheck.radius = KingdomStats.Instance.m_KingdomRadius;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            buildRangeIndicator.SetActive(false);
            UIManager.Instance.openExploringOverlay();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            buildRangeIndicator.SetActive(true);
            UIManager.Instance.openKingdomOverlay();
        }
    }
}
