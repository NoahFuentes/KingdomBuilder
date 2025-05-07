using UnityEngine;
using TMPro;

public class BuildingPopUp : MonoBehaviour
{
    private Transform building;
    private RectTransform rectTransform;
    [SerializeField] private Canvas canvas;

    [SerializeField] private string interactionTxt;
    [SerializeField] private string assignTxt;
    [SerializeField] private string destroyTxt;

    [SerializeField] private TextMeshProUGUI headerTxt;

    public void SetBuilding(Transform setBuilding)
    {
        building = setBuilding;
    }

    public void SetTextInt()
    {
        headerTxt.text = interactionTxt;
    }
    public void SetTextAssign()
    {
        headerTxt.text = assignTxt;
    }
    public void SetTextDestroy()
    {
        headerTxt.text = destroyTxt;
    }


    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        if(null != building)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(building.position + Vector3.up * (building.localScale.y));
            Vector2 canvasPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                screenPos,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main,
                out canvasPos
            );

            rectTransform.anchoredPosition = canvasPos;
        }
    }
}
