using UnityEngine;
using System.Collections;

public class SCRIPT_swordHit : MonoBehaviour {
    
    [SerializeField]
    enemyNavigation enemyController;

    void OnTriggerEnter(Collider playerCollider)
    {
        EnemyStats enemyStats = enemyController.getEnemyStats();
        PlayerStats playerStats = playerCollider.GetComponent<SCRIPT_avatarStats>().getPlayerStats();
        playerStats.takeDamage(enemyStats.getStrength());
    }
}
