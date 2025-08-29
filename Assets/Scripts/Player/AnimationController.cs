using UnityEngine;

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
        CharacterMovement.Instance.EnableMovement();
        animator.SetBool("isAttacking", false);
    }
    public void CheckHitBoxAnimEvent()
    {
        Weapon_SO weapon = PlayerStats.Instance.m_CurrentWeapon;
        Collider[] enemiesHit = Physics.OverlapBox(attackTrans.position, weapon.attackDimensions / 2, transform.rotation, attackMask);
        if(enemiesHit.Length > 0)
            Draw.Instance.DrawBox(attackTrans.position, transform.rotation, weapon.attackDimensions, Color.red);
        else
            Draw.Instance.DrawBox(attackTrans.position, transform.rotation, weapon.attackDimensions, Color.blue);

        foreach(Collider enemy in enemiesHit)
        {
            //bad spot for this, but this is where we damgage enemies...
            enemy.GetComponent<MobAI>().TakeDamage(weapon.damage);
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

    public void PlayFootStepSound()
    {
        PlayerAudioManager.Instance.PlayFootStep();
    }
    public void PlayAttackSound()
    {
        PlayerAudioManager.Instance.PlayClipByName("attack");
    }
}
