using UnityEngine;
using System.Collections;

public class enemyHit : MonoBehaviour
{

    [SerializeField]
    Animator anim;

    [SerializeField]
    Collider weaponCollider;

    private bool isHitting;

    void Start()
    {
        isHitting = false;
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
