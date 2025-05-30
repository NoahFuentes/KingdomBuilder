using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    public Collider weaponCollider;

    private void Start()
    {
        animator = GetComponent<Animator>();
        weaponCollider.enabled = false;
    }

    public void StartAttackAnimEvent()
    {
        if (null == weaponCollider) weaponCollider = GameObject.FindGameObjectWithTag("WorldSpaceWeapon").GetComponent<Collider>();
        animator.SetBool("isAttacking", true);
    }
    public void FinishAttackAnimEvent()
    {
        CharacterMovement.Instance.EnableMovement();
        animator.SetBool("isAttacking", false);
    }
    public void EnableHitBoxAnimEvent()
    {
        weaponCollider.enabled = true;
    }
    public void DisableHitBoxAnimEvent()
    {
        weaponCollider.enabled = false;
    }
}
