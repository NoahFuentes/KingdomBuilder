using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingBtn : MonoBehaviour
{
    [SerializeField] private Building_SO buildingInfo;

    [SerializeField] private Image buildingIcon;
    [SerializeField] private TextMeshProUGUI buildingName;
    [SerializeField] private TextMeshProUGUI buildingDesc;

    //[SerializeField] private Sprite[] resourceIcons;
    [SerializeField] private TextMeshProUGUI[] resourceCosts;



    private void Start()
    {
        buildingIcon.sprite = buildingInfo.buildingIcon;
        buildingName.text = buildingInfo.buildingName;
        buildingDesc.text = buildingInfo.buildingDesc;

        for (int i = 0; i < buildingInfo.resources.Length; i++)
            resourceCosts[i].text = buildingInfo.costs[i].ToString();
    }
}
