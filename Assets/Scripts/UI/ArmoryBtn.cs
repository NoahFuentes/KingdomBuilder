using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArmoryBtn : MonoBehaviour
{
    [SerializeField] private Weapon_SO weaponInfo;

    [SerializeField] private Image weaponIcon;
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI weaponDesc;
    [SerializeField] private TextMeshProUGUI weaponDmg;
    [SerializeField] private TextMeshProUGUI weaponStamCost;

    [SerializeField] private Image[] resourceIcons;
    [SerializeField] private TextMeshProUGUI[] resourceCosts;


    //UNITY FUNCTIONS

    private void Start()
    {
        weaponIcon.sprite = weaponInfo.weaponIcon;
        weaponName.text = weaponInfo.weaponName + " (" + weaponInfo.dmgType + ")";
        weaponDesc.text = weaponInfo.weaponDesc;

        weaponDmg.text = weaponInfo.damage.ToString();
        weaponStamCost.text = weaponInfo.staminaCost.ToString();

        for (int i = 0; i < resourceCosts.Length; i++)
        {
            if (i > weaponInfo.costs.Length - 1)
            {
                resourceCosts[i].text = "";
                resourceIcons[i].enabled = false;
                continue;
            }
            resourceCosts[i].text = weaponInfo.costs[i].ToString();
        }
    }

    private void OnEnable()
    {
        GetComponent<Button>().interactable = KingdomStats.Instance.CanAfford(weaponInfo.resources, weaponInfo.costs);

    }
}
