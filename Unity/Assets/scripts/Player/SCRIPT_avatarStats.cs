using UnityEngine;
using System.Collections;

public class SCRIPT_avatarStats : MonoBehaviour {

    [SerializeField]
    PlayerController playerController;

    public PlayerStats getPlayerStats()
    {
        return playerController.getPlayerStats();
    }
}
