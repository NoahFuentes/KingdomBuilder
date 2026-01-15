using UnityEngine;
using System.Collections.Generic;

public class HitBox : MonoBehaviour
{
    public int damage;
    public DamageType damageType;

    public LayerMask hitMask;

    private HashSet<IHurtBox> hitSet = new();

    public void ResetHitSet()
    {
        hitSet.Clear();
    }


    private void OnTriggerEnter(Collider other)
    {
        if ((hitMask.value & (1 << other.gameObject.layer)) == 0) return;
        if (other.transform.parent.TryGetComponent<IHurtBox>(out var hurtBox))
        {
            if (hitSet.Add(hurtBox))
            {
                hurtBox.TakeHit(damage, damageType);
            }
        }
    }
}
