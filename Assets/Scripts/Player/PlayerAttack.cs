using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInteractions pi;
    private Rigidbody rb;
    private Animator animator;
    [SerializeField] private LayerMask groundLayer;

    private bool isAttacking;

    private float lastAttackedTime;


    public void StartAttack(Vector3 direction)
    {
        //BAIL CASES
        if (isAttacking) return;
        //check for attack spamming
        if (Time.time - lastAttackedTime < PlayerStats.Instance.m_CurrentWeapon.attackTime) return;
        //check stamina and remove it
        if (PlayerStats.Instance.m_CurrentStamina < PlayerStats.Instance.m_CurrentWeapon.staminaCost) return;

        lastAttackedTime = Time.time;
        pi.ReduceStamina(PlayerStats.Instance.m_CurrentWeapon.staminaCost);
        //disable movement and stop animations
        CharacterMovement.Instance.DisableMovement();
        animator.SetBool("isMining", false);
        animator.SetBool("isChopping", false);
        //face attack direction
        FaceAttackDir(direction);
        //start dash (dash animation should check for collision or end of range and attack)
        Attack();
    }
    public void FaceAttackDir(Vector3 dir)
    {
        dir.y = transform.position.y;
        Vector3 direction = dir - transform.position;
        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
        }
    }
    /*
    private IEnumerator Dash(float distance, float speed, Vector3 dir)
    {
        collisionDetected = false;
        isAttacking = true;
        //animator.SetTrigger("dash");
        Vector3 startPos = transform.position;

        while ((Vector3.Distance(transform.position, startPos) <= distance) && !collisionDetected)
        {
            rb.MovePosition(transform.position + (dir * (speed * Time.deltaTime)));
            yield return null;
        }
        //Debug.Log("col detect: " + collisionDetected);
        isAttacking = false;
    }
    */
    private void Attack()
    {
        animator.SetTrigger("attack"); //attack animation needs to set isAttacking to false
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

    //UNITY FUNCTIONS

    private void Start()
    {
        pi = GetComponent<PlayerInteractions>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        
        isAttacking = false;
        lastAttackedTime = Time.time;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 500, groundLayer))
            {
                StartAttack(hit.point);
            }
        }
    }
}
