using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private PlayerStats ps;
    private CharacterMovement movement;
    private Animator animator;
    private UIManager ui;
    private WeaponManager wm;

    private float lastDamagedTime;
    private float lastUsedStaminaTime;

    [SerializeField] private Transform rightItemSpawn;
    [SerializeField] private Transform leftItemSpawn;



    private void Start()
    {
        ps = GetComponent<PlayerStats>();
        movement = GetComponent<CharacterMovement>();
        animator = GetComponent<Animator>();
        ui = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        wm = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();

        ui.updateHealthBarMaxValue(ps.m_MaxHealth);
        ui.updateHealthBarCurrentValue(ps.m_CurrentHealth);
        ui.updateStaminaBarMaxValue(ps.m_MaxStamina);
        ui.updateStaminaBarCurrentValue(ps.m_CurrentStamina);

        EquipWeapon("Stick");
    }

    private void FixedUpdate()
    {
        if (!ps.m_ShouldRegenHealth && Time.time - lastDamagedTime > ps.m_HealthRegenDelay)
            ps.m_ShouldRegenHealth = true;
        if (ps.m_CurrentHealth < ps.m_MaxHealth && (ps.m_ShouldRegenHealth))
        {
            HealPlayer(ps.m_HealthRegenAmount);
        }

        if (!ps.m_ShouldRegenStamina && Time.time - lastUsedStaminaTime > ps.m_StaminaRegenDelay)
            ps.m_ShouldRegenStamina = true;
        if (ps.m_CurrentStamina < ps.m_MaxStamina && (ps.m_ShouldRegenStamina))
        {
            RecoverStamina(ps.m_StaminaRegenAmount);
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //    EquipWeapon("Test");
        
    }


    #region Stat managment
    public void HealPlayer()
    {
        ps.m_CurrentHealth++;
        ui.updateHealthBarCurrentValue(ps.m_CurrentHealth);
    }
    public void HealPlayer(float amt)
    {
        ps.m_CurrentHealth += amt;
        ui.updateHealthBarCurrentValue(ps.m_CurrentHealth);
    }
    public void TakeDamage(ushort damage)
    {
        ps.m_ShouldRegenHealth = false;
        lastDamagedTime = Time.time;
        ps.m_CurrentHealth -= damage;
        ui.updateHealthBarCurrentValue(ps.m_CurrentHealth);
        if (ps.m_CurrentHealth <= 0)
            killPlayer();
    }

    public void ReduceStamina(float amt)
    {
        ps.m_ShouldRegenStamina = false;
        lastUsedStaminaTime = Time.time;
        ps.m_CurrentStamina -= amt;
        ui.updateStaminaBarCurrentValue(ps.m_CurrentStamina);
    }
    public void RecoverStamina()
    {
        ps.m_CurrentStamina++;
        ui.updateStaminaBarCurrentValue(ps.m_CurrentStamina);
    }
    public void RecoverStamina(float amt)
    {
        ps.m_CurrentStamina += amt;
        ui.updateStaminaBarCurrentValue(ps.m_CurrentStamina);
    }


    public void killPlayer()
    {
        gameObject.SetActive(false);
    }
    #endregion

    #region Combat

    /*
    public void TargetedAttack(Transform target)
    {
        movement.canMove = false;
        if (animator.GetBool("isAttacking")) return;
        if (Vector3.Distance(transform.position, target.position) > ps.m_CurrentWeapon.attackRange) return;

        FaceAttackDir(target.position);
        animator.SetTrigger("attack");

        Debug.Log("Attacked target with " + ps.m_CurrentWeapon.weaponName);
        Debug.Log("Damage: " + ps.m_CurrentWeapon.damage);
        Debug.Log("Dmg Type: " + ps.m_CurrentWeapon.dmgType);
        Debug.Log("Range: " + ps.m_CurrentWeapon.attackRange);
        Debug.Log("Type: " + ps.m_CurrentWeapon.weaponType);
    }
    */
    /*
    public void NonTargetedAttack(Vector3 direction)
    {
        if (animator.GetBool("isAttacking")) return;
        movement.DisableMovement();
        FaceAttackDir(direction);
        animator.SetTrigger("attack"); //players swing animation

        Debug.Log("Attacked with " + ps.m_CurrentWeapon.weaponName);
        Debug.Log("Damage: " + ps.m_CurrentWeapon.damage);
        Debug.Log("Dmg Type: " + ps.m_CurrentWeapon.dmgType);
        Debug.Log("Range: " + ps.m_CurrentWeapon.attackRange);
        Debug.Log("Type: " + ps.m_CurrentWeapon.weaponType);
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
    */
    public void EquipWeapon(string weaponName)
    {
        Weapon_SO weapon = wm.GetWeaponDetailsByName(weaponName);
        ps.m_CurrentWeapon = weapon;
        animator.runtimeAnimatorController = weapon.animController;
        if(rightItemSpawn.childCount != 0)
        {
            //remove old weapon model
            Destroy(rightItemSpawn.GetChild(0).gameObject);
        }
        if (leftItemSpawn.childCount != 0)
        {
            //remove old weapon model
            Destroy(leftItemSpawn.GetChild(0).gameObject);
        }
        //add new weapon model
        if (null != weapon.modelRight)
            Instantiate(weapon.modelRight, rightItemSpawn);
        if(null != weapon.modelLeft)
            Instantiate(weapon.modelLeft, leftItemSpawn);
        GetComponent<AnimationController>().weaponCollider = GameObject.FindGameObjectWithTag("WorldSpaceWeapon").GetComponent<Collider>();
        //TODO: update UI sprite
    }

    #endregion

}
