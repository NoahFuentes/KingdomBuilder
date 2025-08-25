using UnityEngine;
public class AttackingMobAI : MobAI //parent class for all Mob AI that can attack (hostile and neutral mobs)
{
    public bool isAttacking = false;
    [SerializeField] private Transform attackTrans;
    
    public void SetIsAttackingTrue()
    {
        isAttacking = true;
    }
    public void SetIsAttackingFalse()
    {
        isAttacking = false;
    }


    public void CheckAttackHitBox()
    {
        Collider[] enemiesHit = Physics.OverlapBox(attackTrans.position, stats.attackDimensions / 2, transform.rotation, targetMask);
        if (enemiesHit.Length > 0)
            Draw.Instance.DrawBox(attackTrans.position, transform.rotation, stats.attackDimensions, Color.red);
        else
            Draw.Instance.DrawBox(attackTrans.position, transform.rotation, stats.attackDimensions, Color.blue);

        foreach (Collider enemy in enemiesHit)
        {
            //bad spot for this, but this is where we damage targets...
            enemy.GetComponent<MobAI>()?.TakeDamage(stats.damage);
            enemy.GetComponent<PlayerInteractions>()?.TakeDamage(stats.damage);
        }
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInteractions.Instance.TakeDamage(stats.damage);
        }
    }
    */
}
