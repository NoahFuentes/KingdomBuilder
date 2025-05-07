using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BuildingPopUpButton : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private string hoverString;
    [SerializeField] private TextMeshProUGUI headerText;
    public void OnPointerEnter(PointerEventData eventData)
    {
        headerText.text = hoverString;
    }
}
