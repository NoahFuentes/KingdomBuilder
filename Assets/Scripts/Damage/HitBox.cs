using UnityEngine;
using System.Collections.Generic;

public class HitBox : MonoBehaviour
{
    public int damage;
    public DamageType damageType;

    private HashSet<IHurtBox> hitSet = new();

    private void OnEnable()
    {
        hitSet.Clear();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.parent) return;
        if (other.transform.parent.TryGetComponent<IHurtBox>(out var hurtBox))
        {
            if (hitSet.Add(hurtBox))
            {
                hurtBox.TakeHit(damage, damageType);
            }
        }
    }
}
