using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SCRIPT_StatsAllocation : MonoBehaviour
{
    [SerializeField]
    Text skillPointsText;

    [SerializeField]
    Text healthText;

    [SerializeField]
    Text staminaText;

    [SerializeField]
    Text strengthText;

    SCRIPT_dataManager dataManager = new SCRIPT_dataManager();
    PlayerStats playerStats;
    int skillPoints;
    int health;
    int stamina;
    int strength;

    // Use this for initialization
    void Start ()
    {
        playerStats = dataManager.loadProfile();

        skillPoints = 20;
        health = playerStats.getHealth();
        stamina = playerStats.getStamina();
        strength = playerStats.getStrength();
    }

    void Update()
    {
        skillPointsText.text = "" + skillPoints;
        healthText.text = "" + health;
        staminaText.text = "" + stamina;
        strengthText.text = "" + strength;
    }
	
	public void addHealth()
    {
        if(skillPoints > 0)
        {
            skillPoints -= 5;
            health += 5;
        }
    }

    public void removeHealth()
    {
        if (health > playerStats.getHealth())
        {
            skillPoints += 5;
            health -= 5;
        }
    }

    public void addStamina()
    {
        if (skillPoints > 0)
        {
            skillPoints -= 5;
            stamina += 5;
        }
    }

    public void removeStamina()
    {
        if (stamina > playerStats.getStamina())
        {
            skillPoints += 5;
            stamina -= 5;
        }
    }

    public void addStrength()
    {
        if (skillPoints > 0)
        {
            skillPoints -= 5;
            strength += 5;
        }
    }

    public void removeStrength()
    {
        if (strength > playerStats.getStrength())
        {
            skillPoints += 5;
            strength -= 5;
        }
    }

    public void saveStats()
    {
        PlayerStats newStats = new PlayerStats(playerStats.getName(), health, stamina, strength);
        SCRIPT_dataManager.saveProfile(newStats);
        SceneManager.LoadScene("MainMenu");
    }
}
