using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MenuManager : MonoBehaviour
{

    void OnLevelWasLoaded(int level)
    {
        if(level == 0)
        {
            NetworkManager.Shutdown();
        }
    }

    private SCRIPT_dataManager dataManager = new SCRIPT_dataManager();

    public void LoadGame()
    {
        SceneManager.LoadScene("NetworkMenu");
    }

    public void NewGame()
    {
        PlayerStats newCharac = new PlayerStats();
        dataManager.saveProfile(newCharac);
        SceneManager.LoadScene("NetworkMenu");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
