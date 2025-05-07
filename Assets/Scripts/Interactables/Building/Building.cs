using UnityEngine;

public class Building : MonoBehaviour
{
    #region Building
    [SerializeField] private string buildingName;
    [SerializeField] protected float interactionBreakDist;
    public bool isOccupied = false;

    [SerializeField] private string[] returnResources;
    [SerializeField] private int[] returnAmts;

    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform interactionDisplay;

    private Vector3 playerPos;

    public virtual void Interaction()
    {
        Debug.Log("Interacting with " + buildingName);

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * (transform.localScale.y));
        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            screenPos,
            canvas.worldCamera,
            out canvasPos
        );

        interactionDisplay.anchoredPosition = canvasPos;
        interactionDisplay.gameObject.SetActive(true);
        interactionDisplay.gameObject.GetComponent<BuildingPopUp>().SetBuilding(this.transform);
    }

    public void DeconstructBuilding()
    {
        KingdomStats ks = GameObject.FindGameObjectWithTag("KingdomManager").GetComponent<KingdomStats>();
        UIManager ui = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        ks.AddResources(returnResources, returnAmts);
        ui.UpdateResourceCounts();
        Destroy(gameObject);
    }
    protected void CloseInteractionDisplay()
    {
        interactionDisplay.gameObject.SetActive(false);
    }
    
    protected void Update()
    {
        if (!interactionDisplay.gameObject.activeSelf) return;
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        if (Vector3.Distance(playerPos, transform.position) > interactionBreakDist)
        {
            CloseInteractionDisplay();
        }
    }

    #endregion
}
