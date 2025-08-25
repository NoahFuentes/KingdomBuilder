using UnityEngine;

public class NeutralMobAI : AttackingMobAI
{
    public bool wasHit = false;

    private void FixedUpdate()
    {
        if (wasHit)
        {
            targets = Physics.OverlapSphere(transform.position, noticeRange, targetMask);
            if (targets.Length > 0)
            {
                float closestTargetDist = Vector3.Distance(transform.position, targets[0].transform.position);
                GameObject currentTarget = targets[0].gameObject;
                for (int i = 0; i < targets.Length; i++)
                {
                    float dist = Vector3.Distance(transform.position, targets[i].transform.position);
                    if (dist <= closestTargetDist)
                    {
                        currentTarget = targets[i].gameObject;
                        closestTargetDist = dist;
                    }
                }
                agent.destination = currentTarget.transform.position;
            }
            else if (Time.time - lastWanderTime >= wanderTime)
            {
                lastWanderTime = Time.time;
                float randX = Random.Range(0, wanderRange);
                float randZ = Random.Range(0, wanderRange);
                Vector3 wanderPosition = new Vector3(randX, 0, randZ) + spawner.transform.position;
                agent.destination = wanderPosition;
            }
        }
        else if (Time.time - lastWanderTime >= wanderTime)
        {
            lastWanderTime = Time.time;
            float randX = Random.Range(0, wanderRange);
            float randZ = Random.Range(0, wanderRange);
            Vector3 wanderPosition = new Vector3(randX, 0, randZ) + spawner.transform.position;
            agent.destination = wanderPosition;
        }
    }

    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);
        /*
        stats.currentHealth -= dmg;
        if (stats.currentHealth <= 0)
        {
            Die();
            return;
        }
        */
        wasHit = true;
    }
}
