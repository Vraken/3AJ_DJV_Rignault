using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SCRIPT_HUD : MonoBehaviour {

    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    Text healthText;

    [SerializeField]
    Text staminaText;

    void Update()
    {
        healthText.text = ""+ playerController.getPlayerStats().getHealth();
        staminaText.text = ""+ playerController.getPlayerStats().getStamina();
    }
}
