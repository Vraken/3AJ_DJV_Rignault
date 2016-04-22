using UnityEngine;
using System.Collections;

public class SCRIPT_playerHit : MonoBehaviour
{
    [SerializeField]
    PlayerStats playerStats;

    void OnTriggerEnter(Collider enemyCollider)
    {
        EnemyStats enemyStats = enemyCollider.GetComponent<EnemyStats>();
        enemyStats.takeDamage(playerStats.getStrength());
    }
}
