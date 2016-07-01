using UnityEngine;
using System.Collections;
using System;

public class SCRIPT_playerHit : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;

    void OnTriggerEnter(Collider enemyCollider)
    {
        EnemyStats enemyStats = new EnemyStats();
        try
        {
            enemyStats = enemyCollider.GetComponent<enemyNavigation>().getEnemyStats();
        }
        catch(Exception e) {
            try
            {
                enemyStats = enemyCollider.GetComponent<SCRIPT_BossIA>().getBossStats();
            }
            catch (Exception ex) { }
        }

        int playerStrength = playerController.getPlayerStats().getStrength();
        enemyStats.takeDamage(playerStrength);
    }
}
