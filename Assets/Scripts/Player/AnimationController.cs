using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private CharacterMovement movement;
    [SerializeField] private Collider dashCollider;

    private void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<CharacterMovement>();
        dashCollider.enabled = false;
    }


    public void StartDash()
    {
        dashCollider.enabled = true;
    }
    public void EndDash()
    {
        dashCollider.enabled = false;
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
