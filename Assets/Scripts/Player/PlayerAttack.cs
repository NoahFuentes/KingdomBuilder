using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInteractions pi;
    private PlayerMovement movement;
    private Animator animator;
    //[SerializeField] private LayerMask attackLayer;

    private bool isAttacking;

    private float lastAttackedTime;


    public void Attack()
    {
        //BAIL CASES
        if (isAttacking) return;
        //check for attack spamming
        if (Time.time - lastAttackedTime < PlayerStats.Instance.m_CurrentWeapon.attackTime) return;
        //check stamina and remove it
        if (PlayerStats.Instance.m_CurrentStamina < PlayerStats.Instance.m_CurrentWeapon.staminaCost)
        {
            NotificationManager.Instance.Notify("Insufficient stamina", Color.red);
            return;
        }
        lastAttackedTime = Time.time;
        pi.ReduceStamina(PlayerStats.Instance.m_CurrentWeapon.staminaCost);
        //disable movement and stop animations
        movement.canMove = false;
        movement.canJump = false;
        animator.SetBool("isMining", false);
        animator.SetBool("isChopping", false);
        //face attack direction
        FaceCameraDir();
        animator.SetTrigger("attack");
        /*
        Debug.Log("Attacked with " + ps.m_CurrentWeapon.weaponName);
        Debug.Log("Damage: " + ps.m_CurrentWeapon.damage);
        Debug.Log("Dmg Type: " + ps.m_CurrentWeapon.dmgType);
        Debug.Log("Range: " + ps.m_CurrentWeapon.attackRange);
        Debug.Log("Type: " + ps.m_CurrentWeapon.weaponType);
        Debug.Log("Dash Distance: " + ps.m_CurrentWeapon.dashDistance);
        Debug.Log("Dash Speed: " + ps.m_CurrentWeapon.dashSpeed);
        Debug.Log("Knockback: " + ps.m_CurrentWeapon.knockBackDist);
        */
    }
    public void FaceCameraDir()
    {
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        if (camForward != Vector3.zero)
        {
            // Target rotation to face the camera direction
            Quaternion targetRotation = Quaternion.LookRotation(camForward);
            transform.rotation = targetRotation;
        }
    }

    //UNITY FUNCTIONS

    private void Start()
    {
        pi = GetComponent<PlayerInteractions>();
        movement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        
        isAttacking = false;
        lastAttackedTime = Time.time;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() || UIManager.Instance.interactingWithUI || !movement.isGrounded) return;
            
            Attack();
        }
    }
}