using UnityEngine;
using System.Collections;

public class hitController : MonoBehaviour {

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
        if(Input.GetButton("Fire1") && !isHitting)
        {
            StartCoroutine(Hit());
        }
	}

    IEnumerator Hit()
    {
        this.isHitting = true;
        animController.SetBool("isHitting", true);
        swordCollider.enabled = true;
        yield return new WaitForSeconds(0.33f);
        this.isHitting = false;
        animController.SetBool("isHitting", false);
        swordCollider.enabled = false;
    }
}
