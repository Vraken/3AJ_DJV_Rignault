using UnityEngine;
using System.Collections;

public class SCRIPT_enemySpawner : MonoBehaviour
{
    [SerializeField]
    Transform spawnerTransform;
    
    [SerializeField]
    public SCRIPT_enemyPool pool;

    private bool enemySpawning;

    // Use this for initialization
    void Start () {
        enemySpawning = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if(!enemySpawning && pool.getSpawnCount() < 50 && pool.getTotalCount() < 100)
        {
            StartCoroutine(Spawn());
        }
	}

    IEnumerator Spawn()
    {
        enemySpawning = true;
        var enemy = SCRIPT_enemyPool.Instance.GetNextAvailableEnemy();
        pool.incrementCount();
        enemy.transform.position = spawnerTransform.position;
        enemy.SetActive(true);
        yield return new WaitForSeconds(2);
        enemySpawning = false;
    }
}
