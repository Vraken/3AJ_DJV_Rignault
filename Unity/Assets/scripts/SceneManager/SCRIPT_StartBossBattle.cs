using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SCRIPT_StartBossBattle : MonoBehaviour
{
    [SerializeField]
    GameObject bossGO;

    [SerializeField]
    GameObject door;

    [SerializeField]
    Transform[] players;

    [SerializeField]
    Transform[] spawners;

    [SerializeField]
    PlayerController[] controllers;

    Dictionary<Transform, Transform> playersSpawn = new Dictionary<Transform, Transform>();
    Dictionary<Transform, PlayerController> playersController = new Dictionary<Transform, PlayerController>();

    private PlayerController playerController;

    void Start()
    {
        for (int i = 0; i < players.Length; i++)
        {
            playersSpawn.Add(players[i], spawners[i]);
            playersController.Add(players[i], controllers[i]);
        }
    }

    void OnTriggerEnter(Collider playerCollider)
    {
        foreach (KeyValuePair<Transform, Transform> player in playersSpawn)
        {
            playersController.TryGetValue(player.Key, out playerController);
            if (playerController.getIsActivated())
            {
                player.Key.position = player.Value.position;
            }
        }
        door.SetActive(true);
        bossGO.SetActive(true);
    }
}
