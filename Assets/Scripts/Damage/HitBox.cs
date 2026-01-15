using UnityEngine;
using System.Collections.Generic;

public class HitBox : MonoBehaviour
{
    public int damage;
    public DamageType damageType;

    public List<string> ignoreTags;

    private HashSet<IHurtBox> hitSet = new();

    public void ResetHitSet()
    {
        hitSet.Clear();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (ignoreTags.Contains(other.tag)) return;
        Debug.Log("trigger with: "+ other.name);
        if (other.transform.parent.TryGetComponent<IHurtBox>(out var hurtBox))
        {
            Debug.Log("found hurtbox");
            if (hitSet.Add(hurtBox))
            {
                Debug.Log("added to set");
                hurtBox.TakeHit(damage, damageType);
            }
        }
    }
}
