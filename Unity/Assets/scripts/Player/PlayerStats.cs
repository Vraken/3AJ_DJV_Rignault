﻿using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    private string playerName;
    private int playerHealth;
    private int playerStamina;
    private int playerStrength;

    private bool isDead;
    private bool isRolling;

    public PlayerStats(string name, int health, int stamina, int strength)
    {
        this.playerName = name;
        this.playerHealth = health;
        this.playerStamina = stamina;
        this.playerStrength = strength;
    }

    public PlayerStats()
    {
        this.playerName = "Bob";
        this.playerHealth = 100;
        this.playerStamina = 100;
        this.playerStrength = 50;
    }

	// Use this for initialization
	void Start () {
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
            playerStamina += (int)Mathf.Round(Time.deltaTime * 5);
        }
    }

    public void makeRoll()
    {
        playerStamina -= 25;
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