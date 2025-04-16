using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

    [SerializeField] private GameObject NPCSideBar;
    [SerializeField] private Vector3 NPCActivePos;
    [SerializeField] private Vector3 NPCInactivePos;

    [SerializeField] private GameObject buildMenu;

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


    public void closeBuildMenu()
    {
        buildMenu.SetActive(false);
    }
    public void openBuildMenu()
    {
        buildMenu.SetActive(true);
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



    private void Start()
    {
        kingdomOverlay.SetActive(true);
        exploringOverlay.SetActive(false);
        mainMenu.SetActive(false);


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mainMenu.SetActive(!mainMenu.activeSelf);
        }
    }


}
