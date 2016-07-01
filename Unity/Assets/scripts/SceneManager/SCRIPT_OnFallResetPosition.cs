using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SCRIPT_OnFallResetPosition : MonoBehaviour {

    [SerializeField]
    Transform[] players;

    [SerializeField]
    Collider[] colliders;

    [SerializeField]
    Transform[] spawners;

    Dictionary<Transform, Transform> playersSpawn = new Dictionary<Transform, Transform>();
    Dictionary<Collider, Transform> playersCollider = new Dictionary<Collider, Transform>();
    private Transform playerTransform;
    private Transform playerSpawner;


    void Start()
    {
        for(int i = 0; i < players.Length; i++)
        {
            playersSpawn.Add(players[i], spawners[i]);
            playersCollider.Add(colliders[i], players[i]);
        }
    }

	void OnTriggerEnter(Collider playerCollider)
    {
        playersCollider.TryGetValue(playerCollider, out playerTransform);
        playersSpawn.TryGetValue(playerTransform, out playerSpawner);
        playerTransform.position = playerSpawner.position;
    }
}
