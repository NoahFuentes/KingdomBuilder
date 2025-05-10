using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private LayerMask hitMask;
    private PlayerStats ps;
    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & hitMask) != 0)
        {
            other.gameObject.GetComponent<MobStats>().TakeDamage(ps.m_CurrentWeapon.damage);
        }
    }
}
