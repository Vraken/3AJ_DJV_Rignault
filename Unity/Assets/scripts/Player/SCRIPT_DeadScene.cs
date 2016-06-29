using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SCRIPT_DeadScene : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(YouDied());
	}
	
	IEnumerator YouDied()
    {
        yield return new WaitForSeconds(5);
        Network.Disconnect();
    }
}
