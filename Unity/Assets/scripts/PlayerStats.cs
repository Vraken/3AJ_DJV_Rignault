using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    private int playerHealth;
    private float playerStamina;
    private int playerStrength;

    private bool isDead;
    private bool isRolling;
    
	// Use this for initialization
	void Start () {
        playerHealth = 100;
        playerStamina = 100f;
        playerStrength = 50;
        isDead = false;
	}
	
	public void takeDamage(int dmg)
    {
        playerHealth -= dmg;
        
        if(playerHealth <= 0)
        {
            isDead = true;
        }
    }

    public bool getIsDead()
    {
        return isDead;
    }

    public void staminaRegen()
    {
        if(playerStamina < 100)
        {
            playerStamina += Time.deltaTime * 5;
        }
    }

    public void makeRoll()
    {
        playerStamina -= 25;
    }

    public float getStamina()
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
}
