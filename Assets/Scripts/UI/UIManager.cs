using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    #region OVERLAY TOGGLING
    [SerializeField] private GameObject kingdomOverlay;
    [SerializeField] private GameObject exploringOverlay;
    [SerializeField] private GameObject mainMenu;

    public void openKingdomOverlay()
    {
        kingdomOverlay.SetActive(true);
        exploringOverlay.SetActive(false);
    }
    public void openExploringOverlay()
    {
        kingdomOverlay.SetActive(false);
        exploringOverlay.SetActive(true);
    }
    #endregion
    #region KINGDOM OVERLAY

    [SerializeField] private TextMeshProUGUI[] kingdomResourceCountStrings;
    [SerializeField] private GameObject buildMenu;

    [SerializeField] private GameObject buildingInfoFooter;
    public GameObject interactionButton;
    [SerializeField] private GameObject upgradeButton;
    [SerializeField] private GameObject demolishButton;
    [SerializeField] private TextMeshProUGUI buildingInfoDesc;
    [SerializeField] private TextMeshProUGUI buildingInfoName;
    [SerializeField] private Image buildingInfoImage;


    public void UpdateKingdomResourceCounts()
    {
        int[] resCounts = KingdomStats.Instance.resourceCurrentAmounts;

        if (resCounts.Length != kingdomResourceCountStrings.Length)
        {
            Debug.Log("ERROR: Resource count does not match ui text counts.");
            return;
        }
        for (int i = 0; i < kingdomResourceCountStrings.Length; i++)
        {
            kingdomResourceCountStrings[i].text = resCounts[i].ToString();
        }
    }

    public void OpenBuildingInfoFooter()
    {
        buildingInfoFooter.SetActive(true);
    }
    public void CloseBuildingInfoFooter()
    {
        buildingInfoFooter.SetActive(false);
    }
    public void UpdateBuildingInfoFooter(Building_SO buildingInfo, ushort level)
    {
        interactionButton.SetActive(buildingInfo.interactable);
        upgradeButton.SetActive(buildingInfo.upgradable);
        demolishButton.SetActive(buildingInfo.demolishable);

        buildingInfoDesc.text = buildingInfo.buildingDesc;
        buildingInfoName.text = buildingInfo.buildingName + " lvl. " + level.ToString();
        buildingInfoImage.sprite = buildingInfo.buildingIcon;

    }

    //Building
    public void closeBuildMenu()
    {
        buildMenu.SetActive(false);
    }
    public void openBuildMenu()
    {
        buildMenu.SetActive(true);
    }


    #endregion
    #region CRAFTING MENUS
    public GameObject armoryCraftingMenu;
    public void OpenArmoryCraftingMenu()
    {
        armoryCraftingMenu.SetActive(true);
    }
    public void CloseArmoryCraftingMenu()
    {
        armoryCraftingMenu.SetActive(false);
    }

    public void CallEquipWeapon(Weapon_SO weapon)
    {
        PlayerInteractions.Instance.EquipWeapon(weapon);
    }

    #endregion
    #region EXPLORING OVERLAY
    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private Slider playerStaminaBar;

    public void updateHealthBarMaxValue(float val)
    {
        playerHealthBar.maxValue = val;
    }
    public void updateHealthBarCurrentValue(float val)
    {
        playerHealthBar.value = val;

    }

    public void updateStaminaBarMaxValue(float val)
    {
        playerStaminaBar.maxValue = val;
    }
    public void updateStaminaBarCurrentValue(float val)
    {
        playerStaminaBar.value = val;

    }
    #endregion
    #region Player Inventory UI
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private TextMeshProUGUI[] playerResCountsUIStrings;

    private void TogglePlayerInventory()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }
    public void UpdatePlayerInventoryResourceCount(int resIndex, int newCount, int resMax)
    {
        playerResCountsUIStrings[resIndex].text = newCount.ToString() + "/" + resMax.ToString();
    }
    private void InitializePlayerInventoryResourceCounts()
    {
        int[] maxes = PlayerInventory.Instance.resCountMaxes;
        for (int i = 0; i < playerResCountsUIStrings.Length; i++)
        {
            playerResCountsUIStrings[i].text = "0/" + maxes[i];
        }
    }

    #endregion



    //main functions
    private void Awake()
    {
        Instance = this; 
    }
    private void Start()
    {
        kingdomOverlay.SetActive(true);
        exploringOverlay.SetActive(false);
        mainMenu.SetActive(false);
        inventoryUI.SetActive(false);

        closeBuildMenu();

        InitializePlayerInventoryResourceCounts();
        UpdateKingdomResourceCounts();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mainMenu.SetActive(!mainMenu.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePlayerInventory();
        }
    }


}
