using UnityEngine;
using UnityEngine.UI;
using StarterAssets;
using System.Collections.Generic;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    #region GENERAL 


    public Sprite waterIcon;
    public Sprite foodIcon;

    public Sprite timberIcon;
    public Sprite lumberIcon;
    public Sprite fineLumberIcon;

    public Sprite clothIcon;
    public Sprite leatherIcon;

    public Sprite roughStoneIcon;
    public Sprite cutStoneIcon;
    public Sprite polishedBrickIcon;

    public Sprite copperOreIcon;
    public Sprite copperIcon;
    public Sprite ironOreIcon;
    public Sprite ironIcon;
    public Sprite goldOreIcon;
    public Sprite goldIcon;

    public Sprite bloodShardIcon;
    public Sprite bloodIronIcon;
    public Sprite geodeIcon;
    public Sprite crysenyxIcon;
    public Sprite gravenScrapIcon;
    public Sprite gravenSteelIcon;

    public Sprite yellowEssIcon;
    public Sprite blueEssIcon;
    public Sprite redEssIcon;
    public Sprite whiteEssIcon;

    public Sprite artifactIcon;

    public Sprite GetResIconByName(string resName)
    {
        switch (resName)
        {
            case "water":
                return waterIcon;
            case "food":
                return foodIcon;
            case "timber":
                return timberIcon;
            case "lumber":
                return lumberIcon;
            case "fine lumber":
                return fineLumberIcon;
            case "cloth":
                return clothIcon;
            case "leather":
                return leatherIcon;
            case "rough stone":
                return roughStoneIcon;
            case "cut stone":
                return cutStoneIcon;
            case "polished brick":
                return polishedBrickIcon;
            case "copper ore":
                return copperOreIcon;
            case "copper":
                return copperIcon;
            case "iron ore":
                return ironOreIcon;
            case "iron":
                return ironIcon;
            case "gold ore":
                return goldOreIcon;
            case "gold":
                return goldIcon;
            case "blood shard":
                return bloodShardIcon;
            case "bloodiron":
                return bloodIronIcon;
            case "geode":
                return geodeIcon;
            case "crysenyx":
                return crysenyxIcon;
            case "graven scrap":
                return gravenScrapIcon;
            case "gravensteel":
                return gravenSteelIcon;
            case "yellow essence":
                return yellowEssIcon;
            case "blue essence":
                return blueEssIcon;
            case "red essence":
                return redEssIcon;
            case "white essence":
                return whiteEssIcon;
            case "artifact":
                return artifactIcon;
            default:
                Debug.Log("No res name by: " + resName);
                return null;
        }

    }

    #endregion
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
        CloseBuildingInfoFooter();
        EndCursorInteraction();
        kingdomOverlay.SetActive(false);
        exploringOverlay.SetActive(true);
    }
    #endregion
    #region KINGDOM OVERLAY

    [SerializeField] private TextMeshProUGUI[] kingdomResourceCountStrings;
    [SerializeField] private GameObject buildMenu;
    [SerializeField] private Button buildBtn;

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
        StartCursorInteraction();
    }
    public void CloseBuildingInfoFooter()
    {
        buildingInfoFooter.SetActive(false);
        DisableBuildBtn();
    }
    public void UpdateBuildingInfoFooter(Building_SO buildingInfo, ushort level)
    {
        interactionButton.SetActive(buildingInfo.interactable);
        upgradeButton.SetActive(buildingInfo.upgradable);
        demolishButton.SetActive(buildingInfo.demolishable);

        buildingInfoDesc.text = buildingInfo.buildingDesc;
        buildingInfoName.text = buildingInfo.buildingName + " lvl. " + level.ToString();
        interactionButton.GetComponentInChildren<TextMeshProUGUI>().text = buildingInfo.interactButtonString;
        buildingInfoImage.sprite = buildingInfo.buildingIcon;

    }

    public void EndCursorInteraction()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>().cursorInputForLook = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void StartCursorInteraction()
    {
        StarterAssetsInputs sai = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssetsInputs>();
        sai.look = Vector2.zero;
        sai.cursorInputForLook = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    //Building

    public void EnableBuildBtn()
    {
        buildBtn.gameObject.SetActive(true);
    }
    public void DisableBuildBtn()
    {
        buildBtn.gameObject.SetActive(false);
    }
    public void closeBuildMenu()
    {
        //Move camera back to player POV
        CameraZoom.Instance.GoToPlayerView();
        ThirdPersonController tpc = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        tpc.canMove = true;
        tpc.canAttack = true;
        buildMenu.SetActive(false);
        EndCursorInteraction();
    }
    public void openBuildMenu()
    {
        CloseBuildingInfoFooter();
        ThirdPersonController tpc = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        tpc.canMove = false;
        tpc.canAttack = false;
        //Move camera to build POV
        CameraZoom.Instance.GoToBuildingView();
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
