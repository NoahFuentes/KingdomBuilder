using UnityEngine;
using System.Collections.Generic;

public class HitBox : MonoBehaviour
{
    [HideInInspector] public int damage;
    [HideInInspector] public DamageType damageType;

    private HashSet<IHurtBox> hitSet = new();

    public void ResetHitSet()
    {
        hitSet.Clear();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IHurtBox>(out var hurtBox))
        {
            if (hitSet.Add(hurtBox))
            {
                hurtBox.TakeHit(damage, damageType);
            }
        }
    }
}
