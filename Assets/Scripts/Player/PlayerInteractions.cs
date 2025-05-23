using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public static PlayerInteractions Instance { get; private set; }
    private PlayerStats ps;
    private CharacterMovement movement;
    private Animator animator;

    private float lastDamagedTime;
    private float lastUsedStaminaTime;

    [SerializeField] private Transform rightItemSpawn;
    [SerializeField] private Transform leftItemSpawn;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ps = GetComponent<PlayerStats>();
        movement = GetComponent<CharacterMovement>();
        animator = GetComponent<Animator>();

        UIManager.Instance.updateHealthBarMaxValue(ps.m_MaxHealth);
        UIManager.Instance.updateHealthBarCurrentValue(ps.m_CurrentHealth);
        UIManager.Instance.updateStaminaBarMaxValue(ps.m_MaxStamina);
        UIManager.Instance.updateStaminaBarCurrentValue(ps.m_CurrentStamina);

        EquipWeapon(WeaponManager.Instance.GetWeaponDetailsByName("Stick"));
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


    #region Stat managment
    public void HealPlayer()
    {
        ps.m_CurrentHealth++;
        UIManager.Instance.updateHealthBarCurrentValue(ps.m_CurrentHealth);
    }
    public void HealPlayer(float amt)
    {
        ps.m_CurrentHealth += amt;
        UIManager.Instance.updateHealthBarCurrentValue(ps.m_CurrentHealth);
    }
    public void TakeDamage(ushort damage)
    {
        ps.m_ShouldRegenHealth = false;
        lastDamagedTime = Time.time;
        ps.m_CurrentHealth -= damage;
        UIManager.Instance.updateHealthBarCurrentValue(ps.m_CurrentHealth);
        if (ps.m_CurrentHealth <= 0)
            killPlayer();
    }

    public void ReduceStamina(float amt)
    {
        ps.m_ShouldRegenStamina = false;
        lastUsedStaminaTime = Time.time;
        ps.m_CurrentStamina -= amt;
        UIManager.Instance.updateStaminaBarCurrentValue(ps.m_CurrentStamina);
    }
    public void RecoverStamina()
    {
        ps.m_CurrentStamina++;
        UIManager.Instance.updateStaminaBarCurrentValue(ps.m_CurrentStamina);
    }
    public void RecoverStamina(float amt)
    {
        ps.m_CurrentStamina += amt;
        UIManager.Instance.updateStaminaBarCurrentValue(ps.m_CurrentStamina);
    }


    public void killPlayer()
    {
        gameObject.SetActive(false);
    }
    #endregion

    #region Combat
    /*
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
    */
    public void EquipWeapon(Weapon_SO weapon)
    {
        ps.m_CurrentWeapon = weapon;
        animator.runtimeAnimatorController = weapon.animController;
        if (rightItemSpawn.childCount != 0)
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
        if (null != weapon.modelLeft)
            Instantiate(weapon.modelLeft, leftItemSpawn);
        GetComponent<AnimationController>().weaponCollider = GameObject.FindGameObjectWithTag("WorldSpaceWeapon").GetComponent<Collider>();
        //TODO: update UI sprite
    }

    #endregion

}
