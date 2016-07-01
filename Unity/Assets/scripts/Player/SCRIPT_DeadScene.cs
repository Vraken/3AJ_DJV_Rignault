using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SCRIPT_DeadScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(YouDied());
	}
	
	IEnumerator YouDied()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
    }
}
