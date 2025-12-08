using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public static PlayerInteractions Instance { get; private set; }
    private Animator animator;

    private float lastDamagedTime;
    private float lastUsedStaminaTime;

    [SerializeField] private Transform rightItemSpawn;
    [SerializeField] private Transform leftItemSpawn;

    public Resource resourceInteractable;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        UIManager.Instance.updateHealthBarMaxValue(PlayerStats.Instance.m_MaxHealth);
        UIManager.Instance.updateHealthBarCurrentValue(PlayerStats.Instance.m_CurrentHealth);
        UIManager.Instance.updateStaminaBarMaxValue(PlayerStats.Instance.m_MaxStamina);
        UIManager.Instance.updateStaminaBarCurrentValue(PlayerStats.Instance.m_CurrentStamina);

        WeaponManager.Instance.EquipWeapon("Stick");
    }

    private void FixedUpdate()
    {
        if (!PlayerStats.Instance.m_ShouldRegenHealth && Time.time - lastDamagedTime > PlayerStats.Instance.m_HealthRegenDelay)
            PlayerStats.Instance.m_ShouldRegenHealth = true;
        if (PlayerStats.Instance.m_CurrentHealth < PlayerStats.Instance.m_MaxHealth && (PlayerStats.Instance.m_ShouldRegenHealth))
        {
            HealPlayer(PlayerStats.Instance.m_HealthRegenAmount);
        }

        if (!PlayerStats.Instance.m_ShouldRegenStamina && Time.time - lastUsedStaminaTime > PlayerStats.Instance.m_StaminaRegenDelay)
            PlayerStats.Instance.m_ShouldRegenStamina = true;
        if (PlayerStats.Instance.m_CurrentStamina < PlayerStats.Instance.m_MaxStamina && (PlayerStats.Instance.m_ShouldRegenStamina))
        {
            RecoverStamina(PlayerStats.Instance.m_StaminaRegenAmount);
        }
    }


    #region Stat managment
    public void HealPlayer()
    {
        PlayerStats.Instance.m_CurrentHealth++;
        UIManager.Instance.updateHealthBarCurrentValue(PlayerStats.Instance.m_CurrentHealth);
    }
    public void HealPlayer(float amt)
    {
        PlayerStats.Instance.m_CurrentHealth += amt;
        UIManager.Instance.updateHealthBarCurrentValue(PlayerStats.Instance.m_CurrentHealth);
    }
    public void TakeDamage(int damage)
    {
        NotificationManager.Instance.ShowDamageNotification(transform.position, damage, Color.red);
        NotificationManager.Instance.FlashScreenRed();
        PlayerAudioManager.Instance.PlaySoundByName("take damage");
        PlayerStats.Instance.m_ShouldRegenHealth = false;
        lastDamagedTime = Time.time;
        PlayerStats.Instance.m_CurrentHealth -= damage;
        UIManager.Instance.updateHealthBarCurrentValue(PlayerStats.Instance.m_CurrentHealth);
        if (PlayerStats.Instance.m_CurrentHealth <= 0)
            killPlayer();
    }

    public void ReduceStamina(float amt)
    {
        PlayerStats.Instance.m_ShouldRegenStamina = false;
        lastUsedStaminaTime = Time.time;
        PlayerStats.Instance.m_CurrentStamina -= amt;
        UIManager.Instance.updateStaminaBarCurrentValue(PlayerStats.Instance.m_CurrentStamina);
    }
    public void RecoverStamina()
    {
        PlayerStats.Instance.m_CurrentStamina++;
        UIManager.Instance.updateStaminaBarCurrentValue(PlayerStats.Instance.m_CurrentStamina);
    }
    public void RecoverStamina(float amt)
    {
        PlayerStats.Instance.m_CurrentStamina += amt;
        UIManager.Instance.updateStaminaBarCurrentValue(PlayerStats.Instance.m_CurrentStamina);
    }


    public void killPlayer()
    {
        gameObject.SetActive(false);
    }
    #endregion

    /*
    public void EquipWeapon(Weapon_SO weapon)
    {
        PlayerStats.Instance.m_CurrentWeapon = weapon;
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
            AnimationController.Instance.playerWSWeaponR = Instantiate(weapon.modelRight, rightItemSpawn);
        if (null != weapon.modelLeft)
            AnimationController.Instance.playerWSWeaponL = Instantiate(weapon.modelLeft, leftItemSpawn);

        //TODO: update UI sprite
    }
    */

}
