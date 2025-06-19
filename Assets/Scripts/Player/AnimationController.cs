using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Transform attackTrans;
    [SerializeField] private LayerMask attackMask;

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
        Collider[] enemiesHit = Physics.OverlapBox(attackTrans.position, weapon.attackDimensions / 2, Quaternion.identity, attackMask);
        foreach(Collider enemy in enemiesHit)
        {
            //bad spot for this, but this is where we damgage enemies...
            enemy.GetComponent<MobStats>().TakeDamage(weapon.damage);
        }
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
}
