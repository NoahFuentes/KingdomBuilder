using UnityEngine;
public class AttackingMobAI : MobAI //parent class for all Mob AI that can attack (hostile and neutral mobs)
{
    public bool isAttacking = false;
    
    public void SetIsAttackingTrue()
    {
        isAttacking = true;
    }
    public void SetIsAttackingFalse()
    {
        isAttacking = false;
    }


    public void ActivateAttackHitBox()
    {
        stats.attackHitBox.enabled = true;
    }
    public void DeactivateAttackHitBox()
    {
        stats.attackHitBox.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInteractions>().TakeDamage(stats.damage);
        }
    }
}
