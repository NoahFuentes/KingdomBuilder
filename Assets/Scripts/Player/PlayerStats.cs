using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    [Header("Health info")]
    public int m_MaxHealth = 100;
    public float m_CurrentHealth = 100;

    public float m_HealthRegenAmount = 0.01f;
    public float m_HealthRegenDelay = 5f;
    public bool m_ShouldRegenHealth = false;

    [Header("Stamina info")]
    public int m_MaxStamina = 100;
    public float m_CurrentStamina = 100;

    public float m_StaminaRegenAmount = 0.1f;
    public float m_StaminaRegenDelay = 0.5f;
    public bool m_ShouldRegenStamina = false;

    [Header("Movement info")]
    public float m_BaseMovementSpeed = 10;
    public float m_SprintSpeedMult = 1.4f;
    public float m_SprintStaminaCost = 0.1f;

    public Animator m_Animator;

    public Weapon_SO m_CurrentWeapon;
    public Pickaxe m_Pickaxe;
    public Axe m_Axe;

    public Armor m_Armor;
    public Accessory m_Accessory1;
    public Accessory m_Accessory2;
    public Accessory m_Accessory3;

    private void Awake()
    {
        Instance = this;
    }
}
