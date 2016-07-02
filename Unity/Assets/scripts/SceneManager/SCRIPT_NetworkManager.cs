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
            Destroy(NetworkManagerGO);
            SceneManager.LoadScene("MainMenu");
        }
    }
}
