using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    private PlayerStats ps;
    private Rigidbody rb;
    private CharacterMovement movement;
    private Animator animator;
    [SerializeField] private LayerMask groundLayer;



    public void StartAttack(Vector3 direction)
    {
        //add bail cases

        //disable movement
        movement.DisableMovement();
        //face attack direction
        FaceAttackDir(direction);
        //start dash (dash animation should check for collision or end of range and attack)
        //animator.SetTrigger("dash");
        Weapon_SO weapon = ps.m_CurrentWeapon;
        StartCoroutine(Dash(weapon.dashDistance, weapon.dashSpeed, (direction - transform.position).normalized));

        Debug.Log("Attacked with " + ps.m_CurrentWeapon.weaponName);
        Debug.Log("Damage: " + ps.m_CurrentWeapon.damage);
        Debug.Log("Dmg Type: " + ps.m_CurrentWeapon.dmgType);
        Debug.Log("Range: " + ps.m_CurrentWeapon.attackRange);
        Debug.Log("Type: " + ps.m_CurrentWeapon.weaponType);
        Debug.Log("Dash Distance: " + ps.m_CurrentWeapon.dashDistance);
        Debug.Log("Dash Speed: " + ps.m_CurrentWeapon.dashSpeed);
        Debug.Log("Knockback: " + ps.m_CurrentWeapon.knockBackDist);
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
    private IEnumerator Dash(float distance, float speed, Vector3 dir)
    {
        Vector3 startPos = transform.position;

        while (Vector3.Distance(transform.position, startPos) <= distance)
        {
            rb.MovePosition(transform.position + (dir * (speed * Time.deltaTime)));
            yield return null;
        }
        movement.EnableMovement();
    }



    private void Start()
    {
        ps = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody>();
        movement = GetComponent<CharacterMovement>();
        animator = GetComponent<Animator>();
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
