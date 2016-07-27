using UnityEngine;
using System.Collections;

public class PlayerStats {

    private string playerName;
    private int playerHealth;
    private int playerStamina;
    private int playerStrength;
    
    private int maxHealth;
    private int maxStamina;

    private float tmpStamina;

    private bool isDead = false;
    private bool isRolling;
    private bool isGodMode = false;

    public PlayerStats(string name, int health, int stamina, int strength)
    {
        this.playerName = name;
        this.playerHealth = health;
        this.playerStamina = stamina;
        this.playerStrength = strength;
        this.tmpStamina = (float)stamina;
        this.maxHealth = health;
        this.maxStamina = stamina;
    }

    public PlayerStats()
    {
        this.playerName = "Bob";
        this.playerHealth = 100;
        this.playerStamina = 100;
        this.playerStrength = 50;
        this.maxHealth = 100;
        this.maxStamina = 100;
        this.tmpStamina = 100f;
    }

    public void setGodMode()
    {
        isGodMode = true;
    }

	// Use this for initialization
	void Start () {
        isDead = false;
	}
	
	public void takeDamage(int dmg)
    {
        if(!isGodMode)
        {
            playerHealth -= dmg;

            if (playerHealth <= 0)
            {
                isDead = true;
            }
        }
    }

    public bool getIsDead()
    {
        return isDead;
    }

    public void staminaRegen(float sec)
    {
        if(playerStamina < maxStamina)
        {
            tmpStamina += sec * 5;
            playerStamina = (int)Mathf.Round(tmpStamina);
        }
    }

    public void makeRoll()
    {
            playerStamina -= 25;
            this.tmpStamina = (float)playerStamina;
    }

    public int getStamina()
    {
        return playerStamina;
    }

    public int getStrength()
    {
        return playerStrength;
    }

    public int getHealth()
    {
        return playerHealth;
    }

    public string getName()
    {
        return playerName;
    }
}
