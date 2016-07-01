using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;

public class SCRIPT_AvatarPoolManager : NetworkBehaviour
{

    #region UnityCompliant Singleton
    public static SCRIPT_AvatarPoolManager Instance
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
    GameObject[] controllers;

    private Queue<GameObject> availableControllers = null;

    private Queue<GameObject> AvailableControllers
    {
        get
        {
            if (null == availableControllers)
            {
                availableControllers = new Queue<GameObject>(controllers);
            }

            return availableControllers;
        }
    }

    public GameObject GetNextAvailableController()
    {
        if (AvailableControllers.Count <= 0)
        {
            return null;
        }
        return AvailableControllers.Dequeue();
    }

    public void ReleaseController(GameObject controller)
    {
        if (null == controller)
        {
            return;
        }
        AvailableControllers.Enqueue(controller);
    }
}
