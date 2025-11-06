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

    public void SetGOInactive(GameObject go)
    {
        go.SetActive(false);
    }
    public void SetGOActive(GameObject go)
    {
        go.SetActive(true);
    }
    public void ToggleGOActiveState(GameObject go)
    {
        go.SetActive(!go.activeSelf);
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
        //CloseBuildingInfoFooter();
        //EndCursorInteraction();
        kingdomOverlay.SetActive(false);
        exploringOverlay.SetActive(true);
    }
    #endregion
    #region KINGDOM OVERLAY

    [SerializeField] private TextMeshProUGUI[] kingdomResourceCountStrings;

    public bool interactingWithUI = false;

    [SerializeField] private GameObject restorationPrompt;
    [SerializeField] private TextMeshProUGUI resBuildingQuestion;
    [SerializeField] private Image[] resBuildingResImages;
    [SerializeField] private TextMeshProUGUI[] resBuildingCosts;


    //[SerializeField] private GameObject buildMenu;
    //[SerializeField] private Button buildBtn;

    //[SerializeField] private GameObject buildingInfoFooter;
    //public GameObject interactionButton;
    //[SerializeField] private GameObject upgradeButton;
    //[SerializeField] private GameObject demolishButton;
    //[SerializeField] private TextMeshProUGUI buildingInfoDesc;
    //[SerializeField] private TextMeshProUGUI buildingInfoName;
    //[SerializeField] private Image buildingInfoImage;


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
    public void PromptForRestoration(Building_SO buildingInfo)
    {
        resBuildingQuestion.text = "Are you sure you want to restore the " + buildingInfo.buildingName + " for:";
        for (int i = 0; i < buildingInfo.resources.Length; i++)
        {
            resBuildingResImages[i].gameObject.SetActive(true);
            resBuildingResImages[i].sprite = GetResIconByName(buildingInfo.resources[i]);
        }
        for (int i = buildingInfo.resources.Length; i < resBuildingResImages.Length; i++)
        {
            resBuildingResImages[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < buildingInfo.costs.Length; i++)
        {
            resBuildingCosts[i].text = buildingInfo.costs[i].ToString();
        }
        for (int i = buildingInfo.costs.Length; i < resBuildingCosts.Length; i++)
        {
            resBuildingCosts[i].text = "";
        }
        restorationPrompt.SetActive(true);
        StartCursorInteraction();
    }
    public void CloseRestorationPrompt()
    {
        restorationPrompt.SetActive(false);
        EndCursorInteraction();
    }
    public void RestoreBuilding()
    {
        CloseRestorationPrompt();
        NearToPlayerInteraction.Instance.currentFocusedObject.TryGetComponent<Building>(out Building building);
        if (building != null)
            building.RestoreSelf();
    }

    public void EndCursorInteraction()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<StarterAssetsInputs>().cursorInputForLook = true;
        player.GetComponent<ThirdPersonController>().canMove = true;
        player.GetComponent<ThirdPersonController>().canJump = true;
        interactingWithUI = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void StartCursorInteraction()
    {
        interactingWithUI = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        StarterAssetsInputs sai = player.GetComponent<StarterAssetsInputs>();
        sai.look = Vector2.zero;
        sai.cursorInputForLook = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        player.GetComponent<ThirdPersonController>().canMove = false;
        player.GetComponent<ThirdPersonController>().canJump = false;
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



    //unity functions
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

        //closeBuildMenu();

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
