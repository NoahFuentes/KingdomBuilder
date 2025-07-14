using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    //private KingdomStats ks;

    public float gridSize = 1.0f;
    [SerializeField] private int contacts = 0;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float startTime;

    [SerializeField] private GameObject buildingPrefab;
    [SerializeField] private string[] resources;
    [SerializeField] private int[] costs;

    private void Start()
    {
        //ks = GameObject.FindGameObjectWithTag("KingdomManager").GetComponent<KingdomStats>();
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

        if (Input.GetMouseButtonDown(0))
        {
            if (contacts != 0)
            {
                Debug.Log(name + ": Could not place, CONTACT INVALID.");
                Destroy(gameObject);
                return;
            }
            else if (!KingdomStats.Instance.CanAfford(resources, costs))
            {
                Debug.Log(name + ": Could not place, COULD NOT AFFORD.");
                Destroy(gameObject);
                return;
            }
            else if (!WithinBuildRange())
            {
                Debug.Log(name + ": Could not place, NOT IN BUILD RANGE.");
                Destroy(gameObject);
                return;
            }
            Instantiate(buildingPrefab, transform.position, transform.localRotation, GameObject.FindGameObjectWithTag("KingdomManager").transform);
            KingdomStats.Instance.RemoveResources(resources, costs);
            Destroy(gameObject);
        }
    }

    public bool WithinBuildRange()
    {
        GameObject kingdom = GameObject.FindGameObjectWithTag("KingdomManager");
        if (Vector3.Distance(transform.position, kingdom.transform.position) > KingdomStats.Instance.m_KingdomRadius) return false;
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
