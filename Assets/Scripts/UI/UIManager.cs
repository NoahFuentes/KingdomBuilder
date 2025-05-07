using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UIManager : MonoBehaviour
{
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
    [SerializeField] private float smoothing;

    [SerializeField] private GameObject resSideBar;
    [SerializeField] private Vector3 resActivePos;
    [SerializeField] private Vector3 resInactivePos;
    [SerializeField] private TextMeshProUGUI foodCount;
    [SerializeField] private TextMeshProUGUI waterCount;
    [SerializeField] private TextMeshProUGUI woodCount;
    [SerializeField] private TextMeshProUGUI textileCount;
    [SerializeField] private TextMeshProUGUI stoneCount;
    [SerializeField] private TextMeshProUGUI ironCount;
    [SerializeField] private TextMeshProUGUI goldCount;
    [SerializeField] private TextMeshProUGUI crystalCount;
    [SerializeField] private TextMeshProUGUI blackCrystalCount;

    [SerializeField] private GameObject NPCSideBar;
    [SerializeField] private Vector3 NPCActivePos;
    [SerializeField] private Vector3 NPCInactivePos;

    [SerializeField] private GameObject buildMenu;

    //NPC SideBar
    public void CallOpenNPCSideBar()
    {
        StartCoroutine(openNPCSideBar());
    }
    public void CallCloseNPCSideBar()
    {
        StartCoroutine(closeNPCSideBar());
    }
    public IEnumerator openNPCSideBar()
    {
        NPCSideBar.SetActive(true);
        RectTransform tr = NPCSideBar.GetComponent<RectTransform>();
        while (Vector3.Distance(tr.localPosition, NPCActivePos) != 0)
        {
            tr.localPosition = Vector3.MoveTowards(tr.localPosition, NPCActivePos, smoothing * Time.deltaTime);
            yield return null;
        }
    }
    public IEnumerator closeNPCSideBar()
    {
        RectTransform tr = NPCSideBar.GetComponent<RectTransform>();
        while (Vector3.Distance(tr.localPosition, NPCInactivePos) != 0)
        {
            tr.localPosition = Vector3.MoveTowards(tr.localPosition, NPCInactivePos, smoothing * Time.deltaTime);
            yield return null;
        }
        NPCSideBar.SetActive(false);
    }


    //Resource SideBar
    public void CallOpenResSideBar()
    {
        StartCoroutine(openResSideBar());
    }
    public void CallCloseResSideBar()
    {
        StartCoroutine(closeResSideBar());
    }
    public IEnumerator openResSideBar()
    {
        resSideBar.SetActive(true);
        RectTransform tr = resSideBar.GetComponent<RectTransform>();
        while (Vector3.Distance(tr.localPosition, resActivePos) != 0)
        {
            tr.localPosition = Vector3.MoveTowards(tr.localPosition, resActivePos, smoothing * Time.deltaTime);
            yield return null;
        }
    }
    public IEnumerator closeResSideBar()
    {
        RectTransform tr = resSideBar.GetComponent<RectTransform>();
        while (Vector3.Distance(tr.localPosition, resInactivePos) != 0)
        {
            tr.localPosition = Vector3.MoveTowards(tr.localPosition, resInactivePos, smoothing * Time.deltaTime);
            yield return null;
        }
        resSideBar.SetActive(false);
    }

    public void UpdateResourceCounts()
    {
        KingdomStats ks = GameObject.FindGameObjectWithTag("KingdomManager").GetComponent<KingdomStats>();
        foodCount.text = ks.m_CurrentFoodAmount.ToString() + "/" + ks.m_MaxFoodAmount.ToString();
        waterCount.text = ks.m_CurrentWaterAmount.ToString() + "/" + ks.m_MaxWaterAmount.ToString();
        woodCount.text = ks.m_CurrentWoodAmount.ToString() + "/" + ks.m_MaxWoodAmount.ToString();
        textileCount.text = ks.m_CurrentTextileAmount.ToString() + "/" + ks.m_MaxTextileAmount.ToString();
        stoneCount.text = ks.m_CurrentStoneAmount.ToString() + "/" + ks.m_MaxStoneAmount.ToString();
        ironCount.text = ks.m_CurrentIronAmount.ToString() + "/" + ks.m_MaxIronAmount.ToString();
        goldCount.text = ks.m_CurrentGoldAmount.ToString() + "/" + ks.m_MaxGoldAmount.ToString();
        crystalCount.text = ks.m_CurrentCrystalAmount.ToString() + "/" + ks.m_MaxCrystalAmount.ToString();
        blackCrystalCount.text = ks.m_CurrentBlackCrystalAmount.ToString() + "/" + ks.m_MaxBlackCrystalAmount.ToString();
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

    public void InstantiateBuildingPlacement(GameObject buildingPlacer) 
    {
        closeBuildMenu();
        Instantiate(buildingPlacer);
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



    //main functions
    private void Start()
    {
        kingdomOverlay.SetActive(true);
        exploringOverlay.SetActive(false);
        mainMenu.SetActive(false);

        CallCloseNPCSideBar();
        CallCloseResSideBar();
        closeBuildMenu();

        UpdateResourceCounts();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mainMenu.SetActive(!mainMenu.activeSelf);
        }
    }


}
