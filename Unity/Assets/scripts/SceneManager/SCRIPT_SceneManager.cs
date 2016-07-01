using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class SCRIPT_SceneManager : MonoBehaviour {

    [SerializeField]
    SCRIPT_enemyPool pool;

    [SerializeField]
    Camera doorCamera;

    [SerializeField]
    Animator doorController;

    [SerializeField]
    GameObject door;

    [SerializeField]
    SCRIPT_BossIA bossIA;

    private bool isComplete = false;
	
	// Update is called once per frame
	void Update () {
        if(pool.getDeathCount() == 100 && !isComplete)
        {
            isComplete = true;
            doorCamera.enabled = true;
            //doorController.enabled = true;
            //doorController.SetTrigger("open");
            door.SetActive(false);
            StartCoroutine(OpenTheGates());
        }
        try
        {
            EnemyStats bossStats = bossIA.getBossStats();
            if (bossStats.getIsDead())
            {
                StartCoroutine(Victory());
            }
        } catch (Exception e) { }
	}

    IEnumerator OpenTheGates()
    {
        yield return new WaitForSeconds(3);
        doorCamera.enabled = false;
    }

    IEnumerator Victory()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Victory");
    }
}
