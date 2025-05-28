using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private LayerMask hitMask;
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & hitMask) != 0)
        {
            other.gameObject.GetComponent<MobStats>().TakeDamage(PlayerStats.Instance.m_CurrentWeapon.damage);
        }
    }
}
