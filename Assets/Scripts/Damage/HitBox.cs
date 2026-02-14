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
        //if (other.CompareTag("alertTrigger")) return;
        if (!other.transform.parent) return;
        if (other.transform.parent.TryGetComponent<IHurtBox>(out var hurtBox))
        {
            Debug.Log("hit something.");
            if (hitSet.Add(hurtBox))
            {
                Debug.Log("was not in set, now added");
                hurtBox.TakeHit(damage, damageType);
            }
            else
                Debug.Log("in set, doing nothing");
        }
    }
}
