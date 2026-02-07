using UnityEngine;
using StarterAssets;

public class AnimationController : MonoBehaviour
{
    public static AnimationController Instance;
    private Animator animator;

    [SerializeField] private Transform attackTrans;
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
        animator.SetBool("isAttacking", true);
    }
    public void FinishAttackAnimEvent()
    {
        PlayerMovement.Instance.canMove = true;
        PlayerMovement.Instance.canJump = true;
        animator.SetBool("isAttacking", false);
    }
    /* REMOVE WHEN REAL DMG SYSTEM IS IN PLACE
    public void CheckHitBoxAnimEvent()
    {
        Weapon_SO weapon = PlayerStats.Instance.m_CurrentWeapon;
        Collider[] targetsHit = Physics.OverlapBox(attackTrans.position, weapon.attackDimensions / 2, transform.rotation, attackMask);
        if(targetsHit.Length > 0)
            Draw.Instance.DrawBox(attackTrans.position, transform.rotation, weapon.attackDimensions, Color.red);
        else
            Draw.Instance.DrawBox(attackTrans.position, transform.rotation, weapon.attackDimensions, Color.blue);

        foreach(Collider target in targetsHit)
        {
            MobBase mob = target.GetComponent<MobBase>();
            if(null == mob) continue;

            mob.hit.damageToTake = weapon.damage;
            mob.stateMachine.ChangeState(mob.hit);
        }
    }
    */

    public void EnableWeaponHitBox()
    {
        if(playerWSWeaponR)
            playerWSWeaponR.GetComponentInChildren<HitBox>().enabled = true;
        if(playerWSWeaponL)
            playerWSWeaponL.GetComponentInChildren<HitBox>().enabled = true;
    }
    public void DisableWeaponHitBox()
    {
        if(playerWSWeaponR)
            playerWSWeaponR.GetComponentInChildren<HitBox>().enabled = false;
        if(playerWSWeaponL)
            playerWSWeaponL.GetComponentInChildren<HitBox>().enabled = false;
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
