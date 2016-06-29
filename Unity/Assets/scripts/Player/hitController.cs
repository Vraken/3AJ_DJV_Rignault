using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class hitController : NetworkBehaviour {

    [SerializeField]
    Collider swordCollider;

    [SerializeField]
    Animator animController;

    bool isHitting;

	// Use this for initialization
	void Start () {
        isHitting = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer) { return; }

        if(Input.GetButton("Fire1") && !isHitting && isLocalPlayer)
        {
            CmdFireHit();
        }
	}

    [Command]
    public void CmdFireHit()
    {
        if (!Network.isClient)
        {
            isHitting = true;
            swordCollider.enabled = true;
            //animController.SetTrigger("hit");
            StartCoroutine(Hit());
        }
        RpcFireHit();
    }

    [ClientRpc]
    public void RpcFireHit()
    {
        animController.SetTrigger("hit");
    }

    IEnumerator Hit()
    {
        yield return new WaitForSeconds(0.33f);
        isHitting = false;
        swordCollider.enabled = false;
    }
}
