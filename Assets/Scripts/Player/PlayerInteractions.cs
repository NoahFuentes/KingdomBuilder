using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private PlayerStats ps;
    private Animator animator;
    private UIManager ui;
    private WeaponManager wm;

    private float lastDamagedTime;
    private float lastUsedStaminaTime;


    private void Start()
    {
        ps = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
        ui = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        wm = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();

        ui.updateHealthBarMaxValue(ps.m_MaxHealth);
        ui.updateHealthBarCurrentValue(ps.m_CurrentHealth);
        ui.updateStaminaBarMaxValue(ps.m_MaxStamina);
        ui.updateStaminaBarCurrentValue(ps.m_CurrentStamina);

        EquipWeapon(wm.GetWeaponDetailsByName("Stick"));
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
        if (Input.GetKeyDown(KeyCode.Space))
            EquipWeapon(wm.GetWeaponDetailsByName("Bow"));
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

    public void TargetedAttack(Transform target)
    {
        if (Vector3.Distance(transform.position, target.position) > ps.m_CurrentWeapon.attackRange) return;

        FaceAttackDir(target.position);

        Debug.Log("Attacked target with " + ps.m_CurrentWeapon.weaponName);
        Debug.Log("Damage: " + ps.m_CurrentWeapon.damage);
        Debug.Log("Dmg Type: " + ps.m_CurrentWeapon.dmgType);
        Debug.Log("Range: " + ps.m_CurrentWeapon.attackRange);
        Debug.Log("Type: " + ps.m_CurrentWeapon.weaponType);
    }

    public void NonTargetedAttack(Vector3 direction)
    {
        FaceAttackDir(direction);


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

    public void EquipWeapon(Weapon_SO weapon)
    {
        ps.m_CurrentWeapon = weapon;
        animator.runtimeAnimatorController = weapon.animController;
        

        //remove old weapon model
        //add new weapon model
    }

    #endregion

}
