using UnityEngine;

public class HostileMobAI : AttackingMobAI //class for all enemy based mobs (attack on-site)
{
    private void FixedUpdate()
    {
        targets = Physics.OverlapSphere(transform.position, noticeRange, targetMask);
        if (targets.Length > 0)
        {
            GameObject currentTarget = targets[0].gameObject;
            float closestTargetDist = Vector3.Distance(transform.position, currentTarget.transform.position);
            for (int i = 0; i < targets.Length; i++)
            {
                float dist = Vector3.Distance(transform.position, targets[i].transform.position);
                if (dist <= closestTargetDist)
                {
                    currentTarget = targets[i].gameObject;
                    closestTargetDist = dist;
                }
            }
            if (isAttacking)
            {
                transform.LookAt(currentTarget.transform);
                return;
            }
            if (Vector3.Distance(transform.position, currentTarget.transform.position) <= stats.stoppingDistance)
            {
                SetIsAttackingTrue();
                transform.LookAt(currentTarget.transform);
                animator.SetTrigger("attack");
                agent.speed = 0f;
                return;
            }
            agent.speed = stats.sprintMovementSpeed;
            agent.destination = currentTarget.transform.position;
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);
        }
        else if (Time.time - lastWanderTime >= wanderTime)
        {
            lastWanderTime = Time.time;
            float randX = Random.Range(0, wanderRange);
            float randZ = Random.Range(0, wanderRange);
            Vector3 wanderPosition = new Vector3(randX, 0, randZ) + spawner.transform.position;
            agent.speed = stats.baseMovementSpeed;
            agent.destination = wanderPosition;
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
        }
        else if (Vector3.Distance(transform.position, agent.destination) <= stats.stoppingDistance)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
    }
}