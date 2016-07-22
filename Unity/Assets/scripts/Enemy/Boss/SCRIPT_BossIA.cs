using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SCRIPT_BossIA : MonoBehaviour {

    [SerializeField]
    NavMeshAgent nav;

    [SerializeField]
    Transform selfTransform;

    [SerializeField]
    Collider weaponCollider;

    [SerializeField]
    Collider aoeCollider;

    [SerializeField]
    Collider bossCollider;

    [SerializeField]
    Animator bossAnimator;

    [SerializeField]
    Animator deathAnimator;

    Dictionary<GameObject, float> playersDistance = new Dictionary<GameObject, float>();

    [SerializeField]
    GameObject[] players;

    Transform targetPlayer;

    EnemyStats bossStats;

    bool isHitting = false;
    bool isRushing = false;
    bool isDying = false;
    bool isPhase2 = false;
    float timeSinceAction = 3;

    // Use this for initialization
    void OnEnable () {
        bossStats = new EnemyStats(5000, 25);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //If boss is dead, launch animation
        if (bossStats.getIsDead() && !isDying)
        {
            isDying = true;
            Die();
        }

        //If boss dying, do nothing
        if(isDying)
        {
            return;
        }

        //Trigger Phase 2  when half HP for additional move
        if(bossStats.getHealth() < bossStats.getMaxHealth()/2 && !isPhase2)
        {
            isPhase2 = true;
        }

        //Get the target and the information
        targetPlayer = getNearestPlayer();
        nav.SetDestination(targetPlayer.position);
        int distanceFromTarget = (int)nav.remainingDistance;

        //When boss is doing nothing
        if (!isHitting)
        {

            //Always rotate towards player
            Vector3 direction = (targetPlayer.position - selfTransform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            selfTransform.rotation = Quaternion.Slerp(selfTransform.rotation, lookRotation, Time.deltaTime * 10f);

            //Generate rdm to see if boss attacks
            int rdm = Random.Range(1, 100);
            rdm += (int)timeSinceAction;

            if(rdm > 103)
            {
                nav.Stop();

                isHitting = true;
                timeSinceAction = 0;

                //When close, attack or aoe in phase 2
                if (distanceFromTarget < 10)
                {
                    if(isPhase2)
                    {
                        rdm = Random.Range(1, 100);
                        if (rdm > 75)
                        {
                            bossAnimator.SetTrigger("AoESpell");
                            StartCoroutine(AoeSpell());
                        }
                        else
                        {
                            bossAnimator.SetTrigger("Attack");
                            StartCoroutine(Attack());
                        }
                    }
                    else
                    {
                        bossAnimator.SetTrigger("Attack");
                        StartCoroutine(Attack());
                    }
                }
                //Else rush on target
                else
                {
                    bossAnimator.SetTrigger("Rush");
                    StartCoroutine(Rush(targetPlayer));
                }
            }
            //If rdm didn't fire attack, move to target
            else
            {
                if(distanceFromTarget > 5)
                {
                    nav.Resume();
                }
                else
                {
                    nav.Stop();
                }
                isHitting = false;
                timeSinceAction += Time.deltaTime;
            }
        }
        
        //Used to stop the rush and the rushing position
        if (isRushing)
        {
            if (nav.remainingDistance <= 3)
            {
                nav.Stop();
                nav.speed = 5;
                isRushing = false;
                isHitting = false;
                weaponCollider.enabled = false;
                bossAnimator.SetTrigger("Rushed");
            }
        }
    }

    Transform getNearestPlayer()
    {
        float minDistance = 0; ;
        GameObject nearestPlayer = null;

        int count = 0;

        //Generate the dictionary of players and their positions
        foreach (GameObject player in players)
        {
            if (!playersDistance.ContainsKey(player))
            {
                playersDistance.Add(player, Vector3.Distance(player.transform.position, selfTransform.position));
            }
            else
            {
                playersDistance[player] = Vector3.Distance(player.transform.position, selfTransform.position);
            }
        }

        //Search for the closest
        foreach (KeyValuePair<GameObject, float> player in playersDistance)
        {
            if (count == 0)
            {
                minDistance = player.Value;
                nearestPlayer = player.Key;
                count++;
                continue;
            }
            if (player.Value < minDistance)
            {
                minDistance = player.Value;
                nearestPlayer = player.Key;
            }
        }

        return nearestPlayer.transform;
    }

    void Die()
    {
        deathAnimator.enabled = true;
        deathAnimator.SetTrigger("Die");
    }

    IEnumerator Attack()
    {
        weaponCollider.enabled = true;
        yield return new WaitForSeconds(2f);
        weaponCollider.enabled = false;
        isHitting = false;
    }

    IEnumerator AoeSpell()
    {
        yield return new WaitForSeconds(3f);
        aoeCollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        aoeCollider.enabled = false;
        yield return new WaitForSeconds(1.5f);
        isHitting = false;
    }

    IEnumerator Rush(Transform target)
    {
        yield return new WaitForSeconds(1f);
        weaponCollider.enabled = true;
        nav.speed = 40;
        nav.SetDestination(target.position);
        nav.Resume();
        isRushing = true;
    }

    public EnemyStats getBossStats()
    {
        return bossStats;
    }
}
