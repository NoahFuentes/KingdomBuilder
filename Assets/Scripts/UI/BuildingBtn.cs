using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingBtn : MonoBehaviour
{
    [SerializeField] private Building_SO buildingInfo;

    [SerializeField] private Image buildingIcon;
    [SerializeField] private TextMeshProUGUI buildingName;
    [SerializeField] private TextMeshProUGUI buildingDesc;

    [SerializeField] private Image[] resourceIcons;
    [SerializeField] private TextMeshProUGUI[] resourceCosts;


    //UNITY FUNCTIONS

    private void Start()
    {
        buildingIcon.sprite = buildingInfo.buildingIcon;
        buildingName.text = buildingInfo.buildingName;
        buildingDesc.text = buildingInfo.buildingDesc;

        for (int i = 0; i < resourceCosts.Length; i++)
        {
            if (i > buildingInfo.costs.Length-1)
            {
                resourceCosts[i].text = "";
                resourceIcons[i].enabled = false;
                continue;
            }
            resourceCosts[i].text = buildingInfo.costs[i].ToString();
        }
    }
}
