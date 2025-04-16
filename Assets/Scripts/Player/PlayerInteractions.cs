using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private PlayerStats ps;
    private UIManager ui;

    private float lastDamagedTime;
    private float lastUsedStaminaTime;

    private void Start()
    {
        ps = GetComponent<PlayerStats>();
        ui = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();

        ui.updateHealthBarMaxValue(ps.m_MaxHealth);
        ui.updateHealthBarCurrentValue(ps.m_CurrentHealth);
        ui.updateStaminaBarMaxValue(ps.m_MaxStamina);
        ui.updateStaminaBarCurrentValue(ps.m_CurrentStamina);
    }

    private void FixedUpdate()
    {

        if (!ps.m_ShouldRegenHealth && Time.time - lastDamagedTime > ps.m_HealthRegenDelay)
            ps.m_ShouldRegenHealth = true;
        if (ps.m_CurrentHealth < ps.m_MaxHealth && (ps.m_ShouldRegenHealth))
        {
            HealPlayer(ps.m_HealthRegenAmount);
        }

        /*
        if (ps.m_CurrentHealth < ps.m_MaxHealth && (Time.time - lastHealthRegenTime >= ps.m_HealthRegenRate))
        {
            lastHealthRegenTime = Time.time;
            HealPlayer();
        }
        */
        if (!ps.m_ShouldRegenStamina && Time.time - lastUsedStaminaTime > ps.m_StaminaRegenDelay)
            ps.m_ShouldRegenStamina = true;
        if (ps.m_CurrentStamina < ps.m_MaxStamina && (ps.m_ShouldRegenStamina))
        {
            RecoverStamina(ps.m_StaminaRegenAmount);
        }
    }

    private void Update()
    {

    }

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

}
