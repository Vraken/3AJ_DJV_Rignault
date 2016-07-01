using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class SCRIPT_NetworkManager : MonoBehaviour {

    [SerializeField]
    GameObject NetworkManagerGO;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            NetworkManager.Shutdown();
            Destroy(NetworkManagerGO);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
