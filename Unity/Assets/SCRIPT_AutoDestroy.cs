using UnityEngine;
using System.Collections;

public class SCRIPT_AutoDestroy : MonoBehaviour
{

    public void Awake()
    {
        Destroy(this.gameObject);
    }
}
