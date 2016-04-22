using UnityEngine;
using System.Collections;

public class SCRIPT_swordHit : MonoBehaviour {

    [SerializeField]
    EnemyStats enemyStats;

    void OnTriggerEnter(Collider playerCollider)
    {
        PlayerStats playerStats = playerCollider.GetComponent<PlayerStats>();
        playerStats.takeDamage(enemyStats.getStrength());
    }
}
