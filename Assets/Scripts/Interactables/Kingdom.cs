using UnityEngine;

public class Kingdom : MonoBehaviour
{
    [SerializeField] private KingdomStats stats;
    private SphereCollider kingdomBuildCheck;
    private UIManager ui;

    [SerializeField] private GameObject buildRangeIndicator;

    private void Start()
    {
        stats = GetComponent<KingdomStats>();
        kingdomBuildCheck = GetComponent<SphereCollider>();
        ui = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        kingdomBuildCheck.radius = stats.m_KingdomRadius;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            buildRangeIndicator.SetActive(false);
            ui.openExploringOverlay();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            buildRangeIndicator.SetActive(true);
            ui.openKingdomOverlay();
        }
    }
}
