using UnityEngine;
using System.Collections;

public class SCRIPT_enemySpawner : MonoBehaviour
{
    [SerializeField]
    Transform spawnerTransform;

    bool enemySpawning;
    int count;

    // Use this for initialization
    void Start () {
        enemySpawning = false;
        count = 0;
	}
	
	// Update is called once per frame
	void Update () {
	    if(!enemySpawning && count <= 4)
        {
            StartCoroutine(Spawn());
        }
	}

    IEnumerator Spawn()
    {
        enemySpawning = true;
        count++;
        var enemy = SCRIPT_enemyPool.Instance.GetNextAvailableEnemy();
        enemy.transform.position = spawnerTransform.position;
        enemy.SetActive(true);
        yield return new WaitForSeconds(2);
        enemySpawning = false;
    }
}
