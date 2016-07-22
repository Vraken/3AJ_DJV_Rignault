using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemyNavigation : MonoBehaviour {
    [SerializeField]
    GameObject[] players;

    [SerializeField]
    NavMeshAgent nav;

    [SerializeField]
    Transform selfTransform;

    [SerializeField]
    Collider weaponCollider;

    [SerializeField]
    Collider enemyCollider;

    [SerializeField]
    Animator anim;

    [SerializeField]
    public SCRIPT_enemyPool pool;

    Dictionary<GameObject, float> playersDistance = new Dictionary<GameObject, float>();

    Transform targetPlayer;
    EnemyStats enemyStats;

    bool isHitting;
    bool wasMoving;

    // Use this for initialization
    void Start()
    {
        isHitting = false;
        wasMoving = false;
    }

    void OnEnable()
    {
        nav.enabled = false;
        nav.enabled = true;
        enemyStats = new EnemyStats(100, 2);
    }

    // Update is called once per frame
    void Update () {
        //if no health then die
        if (enemyStats.getIsDead())
        {
            StartCoroutine(Die());
        }

        //Get target
        targetPlayer = getNearestPlayer();
        nav.SetDestination(targetPlayer.position);

        //If doing nothing then move towards player
        if (!isHitting && !wasMoving)
        {
            nav.Resume();
            wasMoving = true;
        }
        //If not facing player, move
        else if(isHitting && !Physics.Raycast(nav.transform.position, nav.transform.TransformDirection(Vector3.forward), 1f))
        {
            nav.Resume();
            wasMoving = true;
        }
        //Else stop
        else if(isHitting && Physics.Raycast(nav.transform.position, nav.transform.TransformDirection(Vector3.forward), 1f))
        {
            nav.Stop();
            wasMoving = false;
        }
    }

    void OnTriggerStay()
    {
        if (!isHitting)
        {
            StartCoroutine(Hit());
        }
    }

    IEnumerator Hit()
    {
        isHitting = true;
        anim.SetBool("enemyHit", true);
        weaponCollider.enabled = true;
        yield return new WaitForSeconds(1.0f);
        isHitting = false;
        anim.SetBool("enemyHit", false);
        weaponCollider.enabled = false;
    }

    Transform getNearestPlayer()
    {
        float minDistance = 0; ;
        GameObject nearestPlayer = null;

        int count = 0;

        //Generate dictionary of players and their positions
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

        //Search for the closest player
        foreach (KeyValuePair<GameObject, float> player in playersDistance)
        {
            if(count == 0)
            {
                minDistance = player.Value;
                nearestPlayer = player.Key;
                count++;
                continue;
            }
            if(player.Value < minDistance)
            {
                minDistance = player.Value;
                nearestPlayer = player.Key;
            }
        }

        return nearestPlayer.transform;
    }

    IEnumerator Die()
    {
        //Make enemy die
        enemyCollider.enabled = false;
        anim.SetBool("isDying", true);
        yield return new WaitForSeconds(1);
        anim.SetBool("isDying", false);
        selfTransform.Rotate(-90, 0, 0);
        selfTransform.gameObject.SetActive(false);

        //Release from "busy" enemies
        pool.ReleaseEnemy(selfTransform.gameObject);

        //Reset position
        selfTransform.position = pool.poolTransform.position;
        enemyCollider.enabled = true;
        selfTransform.Rotate(90, 0, 0);
    }

    public EnemyStats getEnemyStats()
    {
        return enemyStats;
    }
}
