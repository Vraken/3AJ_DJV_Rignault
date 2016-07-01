using UnityEngine;
using System.Collections;

public class Boss_hit : MonoBehaviour
{

    [SerializeField]
    SCRIPT_BossIA bossIA;

    void OnTriggerEnter(Collider playerCollider)
    {
        EnemyStats bossStats = bossIA.getBossStats();
        PlayerStats playerStats = playerCollider.GetComponent<SCRIPT_avatarStats>().getPlayerStats();
        playerStats.takeDamage(bossStats.getStrength());
    }
}
