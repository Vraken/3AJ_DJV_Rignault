using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SCRIPT_AoESpell : MonoBehaviour {

    [SerializeField]
    SCRIPT_BossIA bossIA;

    [SerializeField]
    PlayerController[] players;

    [SerializeField]
    Collider[] colliders;

    Dictionary<Collider, PlayerController> playersController = new Dictionary<Collider, PlayerController>();

    private PlayerController playerController;

    void OnEnable()
    {
        for (int i = 0; i < players.Length; i++)
        {
            playersController.Add(colliders[i], players[i]);
        }
    }

    void OnTriggerEnter(Collider playerCollider)
    {
        int bossStrength = bossIA.getBossStats().getStrength();
        playersController.TryGetValue(playerCollider, out playerController);
        playerController.getPlayerStats().takeDamage(bossStrength);
    }
}
