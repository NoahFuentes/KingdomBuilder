using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private CharacterMovement movement;
    public Collider weaponCollider;

    private void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<CharacterMovement>();
        weaponCollider.enabled = false;
    }


    public void StartDash()
    {
    }
    public void EndDash()
    {
    }
    public void StartAttackAnimEvent()
    {
        animator.SetBool("isAttacking", true);
    }
    public void FinishAttackAnimEvent()
    {
        movement.EnableMovement();
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
