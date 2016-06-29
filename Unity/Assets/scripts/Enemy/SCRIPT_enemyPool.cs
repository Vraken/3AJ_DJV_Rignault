using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;

public class SCRIPT_enemyPool : NetworkBehaviour {

    #region UnityCompliant Singleton
    public static SCRIPT_enemyPool Instance
    {
        get;
        private set;
    }

    public virtual void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            return;
        }
        Destroy(this.gameObject);
    }
    #endregion

    [SerializeField]
    public GameObject[] enemies;

    [SerializeField]
    public Transform poolTransform;

    int spawnCount;
    int totalSpawn;
    int deathCount;

    private Queue<GameObject> availableEnemies = null;

    private Queue<GameObject> AvailableEnemies
    {
        get
        {
            if (null == availableEnemies)
            {
                availableEnemies = new Queue<GameObject>(enemies);
            }

            return availableEnemies;
        }
    }

    public GameObject GetNextAvailableEnemy()
    {
        if (AvailableEnemies.Count <= 0)
        {
            return null;
        }

        return AvailableEnemies.Dequeue();
    }

    public void ReleaseEnemy(GameObject enemy)
    {
        if (enemy == null)
        {
            return;
        }
        AvailableEnemies.Enqueue(enemy);
        spawnCount--;
        deathCount++;
    }

    public int getDeathCount()
    {
        return deathCount;
    }

    public int getTotalCount()
    {
        return totalSpawn;
    }

    public int getSpawnCount()
    {
        return spawnCount;
    }

    public void incrementCount()
    {
        spawnCount++;
        totalSpawn++;
    }
}
