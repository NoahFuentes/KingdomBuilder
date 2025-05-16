using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    private KingdomStats ks;

    public float gridSize = 1.0f;
    private bool validPlacement = false;
    [SerializeField] private int contacts = 0;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float startTime;

    [SerializeField] private GameObject buildingPrefab;
    [SerializeField] private string[] resources;
    [SerializeField] private int[] costs;

    private void Start()
    {
        ks = GameObject.FindGameObjectWithTag("KingdomManager").GetComponent<KingdomStats>();
        startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - startTime < 0.1f) return;
        Vector3 gridPos = new Vector3();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 500, groundLayer))
        {
            gridPos = new Vector3(
                Mathf.Round(hit.point.x / gridSize) * gridSize,
                Mathf.Round(hit.point.y / gridSize) * gridSize,
                Mathf.Round(hit.point.z / gridSize) * gridSize
            );
        }

        transform.position = gridPos;

        if (contacts == 0 && ks.CanAfford(resources, costs) && WithinBuildRange()) validPlacement = true;
        else validPlacement = false;

        if (Input.GetMouseButtonDown(0))
        {
            if (!validPlacement)
            {
                Destroy(gameObject);
                return;
            }
            Instantiate(buildingPrefab, transform.position, transform.localRotation, GameObject.FindGameObjectWithTag("KingdomManager").transform);
            ks.RemoveResources(resources, costs);
            Destroy(gameObject);
        }
    }

    public bool WithinBuildRange()
    {
        GameObject kingdom = GameObject.FindGameObjectWithTag("KingdomManager");
        if (Vector3.Distance(transform.position, kingdom.transform.position) > kingdom.GetComponent<KingdomStats>().m_KingdomRadius) return false;
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground")) return;
        contacts++;
    }
    private void OnTriggerExit(Collider other)
    {
        contacts--;
    }
}
