using UnityEngine;
using System.Collections;

public class SCRIPT_GlobalCounter : MonoBehaviour {

    [SerializeField]
    SCRIPT_enemyPool pool;

    [SerializeField]
    Camera doorCamera;

    [SerializeField]
    Animator doorController;
	
	// Update is called once per frame
	void Update () {
        if(pool.getDeathCount() == 100)
        {
            Debug.Log("Bravooooooo");
            doorCamera.enabled = true;
            doorController.enabled = true;
            doorController.SetTrigger("open");
            StartCoroutine(OpenTheGates());
        }
	}

    IEnumerator OpenTheGates()
    {
        yield return new WaitForSeconds(5);
        doorCamera.enabled = false;
        doorController.enabled = false;
    }
}
