using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private CharacterMovement movement;

    private void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<CharacterMovement>();
    }

    public void StartAttack()
    {
        animator.SetBool("isAttacking", true);
    }
    public void FinishAttack()
    {
        movement.EnableMovement();
        animator.SetBool("isAttacking", false);
    }
}
