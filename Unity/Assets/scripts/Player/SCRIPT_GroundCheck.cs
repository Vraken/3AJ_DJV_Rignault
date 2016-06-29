using UnityEngine;
using System.Collections;

public class SCRIPT_GroundCheck : MonoBehaviour {

    [SerializeField]
    Collider coll;

    int count = 0;

    void OnTriggerEnter(Collider coll)
    {
        count++;
    }

    void OnTriggerExit(Collider coll)
    {
        count--;
    }

    public bool IsGrounded()
    {
        if(count == 0)
        {
            return false;
        }
        return true;
    }
        
}
