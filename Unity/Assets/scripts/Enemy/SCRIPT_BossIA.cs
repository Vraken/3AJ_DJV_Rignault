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
    EnemyStats bossStats;

    Dictionary<GameObject, float> playersDistance = new Dictionary<GameObject, float>();
    GameObject[] players;

    Transform targetPlayer;

    bool isHitting = false;
    bool isRushing = false;
    float timeSinceAction = 0;

    // Use this for initialization
    void Start () {
        bossStats.setHealth(10000);
        bossStats.setStrength(25);
	}
	
	// Update is called once per frame
	void Update () {
        if (bossStats.getIsDead())
        {
            Die();
        }

        targetPlayer = getNearestPlayer();
        int distanceFromTarget = (int)nav.remainingDistance;

        if (!isHitting)
        {
            int rdm = Random.Range(1, 100);
            rdm += (int)timeSinceAction;

            if(rdm > 99)
            {
                nav.Stop();

                isHitting = true;
                timeSinceAction = 0;

                rdm = Random.Range(1, 100);
                if(rdm > 75)
                {
                    if(distanceFromTarget < 10)
                    {
                        bossAnimator.SetTrigger("AoESpell");
                        StartCoroutine(AoeSpell());
                    } else
                    {
                        bossAnimator.SetTrigger("Rush");
                        StartCoroutine(Rush(targetPlayer));
                    }
                }
                else
                {
                    bossAnimator.SetTrigger("Attack");
                    StartCoroutine(Attack());
                }
            }
            else if(isRushing)
            {
                if(nav.remainingDistance == 0)
                {
                    nav.Stop();
                    nav.speed = 5;
                    isRushing = false;
                    isHitting = false;
                    weaponCollider.enabled = false;
                    bossAnimator.SetTrigger("Rushed");
                }
            }
            else
            {
                nav.SetDestination(targetPlayer.position);
                nav.Resume();
                isHitting = false;
                timeSinceAction += Time.deltaTime;
            }
        }
    }

    Transform getNearestPlayer()
    {
        float minDistance = 0; ;
        GameObject nearestPlayer = null;

        int count = 0;

        players = GameObject.FindGameObjectsWithTag("Player");

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
        bossAnimator.SetTrigger("Die");
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
        aoeCollider.enabled = false;
        yield return new WaitForSeconds(2f);
        isHitting = false;
    }

    IEnumerator Rush(Transform target)
    {
        yield return new WaitForSeconds(1f);
        weaponCollider.enabled = true;
        nav.speed = 20;
        nav.SetDestination(target.position);
        nav.Resume();
        isRushing = true;
    }
}
