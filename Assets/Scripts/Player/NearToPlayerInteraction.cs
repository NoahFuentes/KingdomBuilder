using UnityEngine;
using TMPro;

public class NearToPlayerInteraction : MonoBehaviour
{
    public static NearToPlayerInteraction Instance;

    public GameObject currentFocusedObject;
    private Transform playerTransform;

    [SerializeField] private float interactionDistance;
    private LayerMask interactionMask;

    [SerializeField] private LayerMask npcMask;
    [SerializeField] private string npcInteractPrompt;
    [SerializeField] private LayerMask buildingRestorationMask;
    [SerializeField] private string buildingInteractPrompt;
    [SerializeField] private LayerMask resourceMask;
    [SerializeField] private string resourceInteractPrompt_pickUp;
    [SerializeField] private string resourceInteractPrompt_mine;
    [SerializeField] private string resourceInteractPrompt_chop;

    [SerializeField] private GameObject interactionPrompt;
    [SerializeField] private TextMeshProUGUI promptString;


    private void ChangeFocusedObject(GameObject objectToFocusOn)
    {
        //if there is a current focus obj, clear it
        if (null != currentFocusedObject)
            ClearFocusedObject();

        //focus on the new obj
        FocusOnNewFocusObject(objectToFocusOn);
    }

    private void ClearFocusedObject()
    {
        if (!currentFocusedObject || !interactionPrompt) return;
        interactionPrompt.SetActive(false);
        currentFocusedObject = null;
    }

    private void FocusOnNewFocusObject(GameObject objectToFocusOn) 
    {
        currentFocusedObject = objectToFocusOn;
        if (null == currentFocusedObject) return;
        if (currentFocusedObject.TryGetComponent<Resource>(out Resource res))
        {
            switch (res.interactType)
            {
                case ResourceType.HARVESTABLE:
                    PromptInteraction(resourceInteractPrompt_pickUp);
                    break;
                case ResourceType.MINEABLE:
                    PromptInteraction(resourceInteractPrompt_mine);
                    break;
                case ResourceType.CHOPPABLE:
                    PromptInteraction(resourceInteractPrompt_chop);
                    break;
            }
        }
        else if (currentFocusedObject.TryGetComponent<Companion>(out Companion companion))
        {
            PromptInteraction(companion.info.companionName);
        }
        else if (currentFocusedObject.TryGetComponent<Building>(out Building building))
        {
            if (building.isRestored) return;
            PromptInteraction(buildingInteractPrompt + " " + building.info.buildingName);
        }

    }

    private void PromptInteraction(string prompt)
    {
        if (!promptString || !interactionPrompt) return;
        promptString.text = prompt;
        interactionPrompt.SetActive(true);
    }

    private void InteractWithFocusedObject()
    {
        if (null == currentFocusedObject) return;
        if (currentFocusedObject.TryGetComponent<Resource>(out Resource res))
        {
            HandleResourceInteraction(res);
        }
        else if (currentFocusedObject.TryGetComponent<Companion>(out Companion companion))
        {
            HandleNPCInteraction(companion);
        }
        else if (currentFocusedObject.TryGetComponent<Building>(out Building building))
        {
            if (building.isRestored) return;
            HandleBuildingInteraction(building.info);
        }
            
    }

    private void HandleResourceInteraction(Resource res)
    {
        if (null == currentFocusedObject || null == res) return;
        Animator animator = GetComponentInParent<Animator>();
        transform.parent.LookAt(currentFocusedObject.transform);
        switch (res.interactType)
        {
            case ResourceType.CHOPPABLE:
                animator.SetBool("isMining", false);
                animator.SetBool("isChopping", true);
                break;
            case ResourceType.MINEABLE:
                animator.SetBool("isChopping", false);
                animator.SetBool("isMining", true);
                break;
            case ResourceType.HARVESTABLE:
                animator.SetBool("isChopping", false);
                animator.SetBool("isMining", false);
                animator.SetTrigger("harvest");
                break;
        }
        res.Interaction();
    } 
    private void HandleNPCInteraction(Companion companion)
    {
        if (null == currentFocusedObject || null == companion) return;
        companion.Talk();
    }
    private void HandleBuildingInteraction(Building_SO buildingInfo)
    {
        if (null == currentFocusedObject || null == buildingInfo) return;
        if (!KingdomStats.Instance.CanAfford(buildingInfo.resources, buildingInfo.costs))
            NotificationManager.Instance.Notify("Cannot afford " + buildingInfo.buildingName, Color.red);
        UIManager.Instance.PromptForRestoration(buildingInfo);
    }

    //UNITY FUNCTIONS

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        interactionMask = npcMask + resourceMask + buildingRestorationMask;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            InteractWithFocusedObject();
    }

    private void FixedUpdate()
    {
        //get all colliders with interactable layer
        Collider[] interactablesInRange = Physics.OverlapSphere(transform.position, interactionDistance, interactionMask);
        if (interactablesInRange.Length < 1)
        {
            ClearFocusedObject();   //if no objects are found, clear the focus object
            return;
        }

        //find the closest one
        Collider closestInteractable = interactablesInRange[0];
        float closestInteractableDist = Vector3.Distance(transform.position, interactablesInRange[0].transform.position);
        foreach (Collider col in interactablesInRange)
        {
            float distToCol = Vector3.Distance(transform.position, col.transform.position);
            if (distToCol < closestInteractableDist)
            {
                closestInteractable = col;
                closestInteractableDist = distToCol;
            }
        }
        //if the closest is not the focus object, change to the closest
        if (closestInteractable.gameObject != currentFocusedObject)
        {
            if ((resourceMask.value & (1 << closestInteractable.gameObject.layer)) != 0)
                ChangeFocusedObject(closestInteractable.transform.parent.gameObject);
            else if ((npcMask.value & (1 << closestInteractable.gameObject.layer)) != 0)
                ChangeFocusedObject(closestInteractable.gameObject);
            else if ((buildingRestorationMask.value & (1 << closestInteractable.gameObject.layer)) != 0)
                ChangeFocusedObject(closestInteractable.transform.parent.gameObject);
            //ChangeFocusedObject(closestInteractable.transform.parent.gameObject);

        }
    }
}
