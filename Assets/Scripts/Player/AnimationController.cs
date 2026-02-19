using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public static AnimationController Instance;
    private Animator animator;

    [SerializeField] private LayerMask attackMask;
    public GameObject playerWSWeaponR;
    public GameObject playerWSWeaponL;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartAttackAnimEvent()
    {
        PlayerMovement.Instance.canMove = false;
        PlayerMovement.Instance.canJump = false;
        GetComponent<PlayerAttack>().ANIMEVENT_FaceCameraDir();
    }
    public void FinishAttackAnimEvent()
    {
        PlayerMovement.Instance.canMove = true;
        PlayerMovement.Instance.canJump = true;
    }

    public void EnableWeaponHitBox()
    {
        if (playerWSWeaponR)
        {
            playerWSWeaponR.GetComponentInChildren<HitBox>().enabled = true;
            playerWSWeaponR.GetComponentInChildren<CapsuleCollider>().enabled = true;
        }
        if (playerWSWeaponL)
        {
            playerWSWeaponL.GetComponentInChildren<HitBox>().enabled = true;
            playerWSWeaponL.GetComponentInChildren<CapsuleCollider>().enabled = true;
        }
    }
    public void DisableWeaponHitBox()
    {
        if (playerWSWeaponR)
        {
            playerWSWeaponR.GetComponentInChildren<HitBox>().enabled = false;
            playerWSWeaponR.GetComponentInChildren<CapsuleCollider>().enabled = false;
        }
        if (playerWSWeaponL)
        {
            playerWSWeaponL.GetComponentInChildren<HitBox>().enabled = false;
            playerWSWeaponL.GetComponentInChildren<CapsuleCollider>().enabled = false;
        }
    }
    public void EnableWeaponTrailR()
    {
        playerWSWeaponR.GetComponentInChildren<TrailRenderer>().emitting = true;
    }
    public void DisableWeaponTrailR()
    {
        playerWSWeaponR.GetComponentInChildren<TrailRenderer>().emitting = false;
    }
    public void EnableWeaponTrailL()
    {
        playerWSWeaponL.GetComponentInChildren<TrailRenderer>().emitting = true;
    }
    public void DisableWeaponTrailL()
    {
        playerWSWeaponL.GetComponentInChildren<TrailRenderer>().emitting = false;
    }

    public void HitResourceAnimEvent()
    {
        Resource res = PlayerInteractions.Instance.resourceInteractable;
        int health = res.TakeDamage(1); //TODO: make this according to player's tool dmg
        if (health <= 0)
        {
            animator.SetBool("isMining", false);
            animator.SetBool("isChopping", false);
        }

    }

    public void PlayPlayerSoundByName(string soundName)
    {
        PlayerAudioManager.Instance.PlaySoundByName(soundName);
    }

    public void PlayGlobalSoundByName(string soundName)
    {
        GameObject.FindGameObjectWithTag("GlobalAudioManager").GetComponent<AudioManager>().PlaySoundByName(soundName);
    }
}
