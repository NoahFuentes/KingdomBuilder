using UnityEngine;
using TMPro;

public class BuildingPopUp : MonoBehaviour
{
    private Transform building;
    private RectTransform rectTransform;
    [SerializeField] private Canvas canvas;

    public void SetBuilding(Transform setBuilding)
    {
        building = setBuilding;
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
