using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SCRIPT_InteractionsController : MonoBehaviour
{

    public UnityEvent Finished;

    // Use this for initialization
    void Start()
    {
        Finished.Invoke();
    }
}
