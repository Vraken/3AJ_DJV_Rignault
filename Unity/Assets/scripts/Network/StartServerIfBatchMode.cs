using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class StartServerIfBatchmode : MonoBehaviour
{
    [SerializeField]
    NetworkManager managerHUD;


    // Use this for initialization
    void Start()
    {
        if (SystemInfo.graphicsDeviceID == 0)
        {
            managerHUD.StartServer();
        }
    }
}

