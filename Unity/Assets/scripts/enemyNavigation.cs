using UnityEngine;
using System.Collections;

public class enemyNavigation : MonoBehaviour {

    [SerializeField]
    Transform player;

    [SerializeField]
    NavMeshAgent nav;

    [SerializeField]
    Collider weaponCollider;

    [SerializeField]
    Animator anim;

    bool isHitting;

    // Use this for initialization
    void Start()
    {
        isHitting = false;
    }

    // Update is called once per frame
    void Update () {
        if (!isHitting)
        {
            nav.SetDestination(player.position);
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
        this.isHitting = true;
        anim.SetBool("enemyHit", true);
        weaponCollider.enabled = true;
        yield return new WaitForSeconds(1.0f);
        this.isHitting = false;
        anim.SetBool("enemyHit", false);
        weaponCollider.enabled = false;
    }
}
