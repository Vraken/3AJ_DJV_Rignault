using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class hitController : NetworkBehaviour {

    [SerializeField]
    Collider swordCollider;

    [SerializeField]
    Animator animController;

    bool isHitting;
    //PlayerStats playerStats = new PlayerStats();

	// Use this for initialization
	void Start () {
        isHitting = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer) { return; }

        if(Input.GetButton("Fire1") && !isHitting)
        {
            StartCoroutine(Hit());
        }
	}

    IEnumerator Hit()
    {
        this.isHitting = true;
        animController.SetBool("isHitting", true);
        swordCollider.enabled = true;
        yield return new WaitForSeconds(0.33f);
        this.isHitting = false;
        animController.SetBool("isHitting", false);
        swordCollider.enabled = false;
    }

    /*
    void OnTriggerEnter(EnemyStats enemyCollider)
    {
        int playerDamage = playerStats.getDamage();
        int enemyHealth = enemyCollider.getHealth();

        if(enemyHealth > 0)
        {
            enemyCollider.takeDamage(playerDamage);
        }
    }*/
}
