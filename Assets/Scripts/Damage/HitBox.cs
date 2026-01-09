using UnityEngine;
using System.Collections.Generic;

public class HitBox : MonoBehaviour
{
    [SerializeField] private int damage;
    private HashSet<HurtBox> hitSet = new();

    public void ResetHitSet()
    {
        hitSet.Clear();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<HurtBox>(out var hurtBox))
        {
            if (hitSet.Add(hurtBox))
            {
                hurtBox.TakeHit(damage);
            }
        }
    }
}
