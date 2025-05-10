using UnityEngine;

public class MouseSelection_old : MonoBehaviour
{
    /*
    [SerializeField] private PlayerInteractions pi;
    [SerializeField] private PlayerStats ps;

    [SerializeField] private Material baseMat;

    [SerializeField] private Material hoverMat_interaction;
    [SerializeField] private Material selectedMat_interaction;
    [SerializeField] private Material hoverMat_enemy;

    [SerializeField] private LayerMask interactionMask;
    [SerializeField] private LayerMask enemyMask;

    [SerializeField] private LayerMask hitMask;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private GameObject lastHoveredObject;

    [SerializeField] private GameObject lastSelectedObject;

    private void Start()
    {
        pi = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteractions>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        hitMask = enemyMask + interactionMask;
    }

    private void Update()
    {
        DetectHoveredObject();
        if (Input.GetMouseButtonDown(0) && !ps.m_CurrentWeapon.hasTargetedAttacks)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 500, groundLayer))
            {
                pi.NonTargetedAttack(hit.point);
            }
        }
    }

    void DetectHoveredObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 500, hitMask))
        {
            GameObject hoveredObject = hit.collider.gameObject;

            if (hoveredObject != lastHoveredObject && hoveredObject != lastSelectedObject)
            {
                ResetLastHoveredObject();

                Renderer renderer = hoveredObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Material[] mats = renderer.materials;
                    if ((interactionMask.value & (1 << hoveredObject.layer)) != 0) {
                        mats[1] = hoverMat_interaction;
                    }
                    else if ((enemyMask.value & (1 << hoveredObject.layer)) != 0)
                    {
                        mats[1] = hoverMat_enemy;
                    }
                    renderer.materials = mats;
                }

                lastHoveredObject = hoveredObject;
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (hoveredObject == lastSelectedObject)
                    return;
                ResetLastSelectedObject();
                lastSelectedObject = hoveredObject;

                Renderer renderer = hoveredObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Material[] mats = renderer.materials;
                    if ((interactionMask.value & (1 << hoveredObject.layer)) != 0)
                    {
                        mats[1] = selectedMat_interaction;
                        renderer.materials = mats;
                    }
                }
            }

            if(Input.GetMouseButtonDown(0) && ps.m_CurrentWeapon.hasTargetedAttacks)
            {
                if ((enemyMask.value & (1 << hoveredObject.layer)) != 0)
                {
                    pi.TargetedAttack(hoveredObject.transform);
                }
            }
        }
        else
        {
            ResetLastHoveredObject();
        }
    }

    void ResetLastHoveredObject()
    {
        if (lastHoveredObject != null && lastHoveredObject != lastSelectedObject)
        {
            Renderer renderer = lastHoveredObject.GetComponent<Renderer>();
            if (renderer != null && 1 < renderer.materials.Length)
            {
                Material[] mats = renderer.materials;
                mats[1] = baseMat;
                renderer.materials = mats;
            }

            lastHoveredObject = null;
        }
    }

    void ResetLastSelectedObject()
    {
        if (lastSelectedObject != null)
        {
            Renderer renderer = lastSelectedObject.GetComponent<Renderer>();
            if (renderer != null && 1 < renderer.materials.Length)
            {
                Material[] mats = renderer.materials;
                mats[1] = baseMat;
                renderer.materials = mats;
            }

            lastSelectedObject = null;
        }
    }
    */
}
