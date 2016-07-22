using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

    private int enemyHealth;
    private int enemyStrength;
    private int enemyMaxHealth;

    private bool isDead;

	// Use this for initialization
	void Start () {
        isDead = false;
	}

    public EnemyStats(int health, int strength)
    {
        this.enemyHealth = health;
        this.enemyMaxHealth = health;
        this.enemyStrength = strength;
    }

    public EnemyStats()
    {
    }

    public void takeDamage(int dmg)
    {
        enemyHealth -= dmg;
        
        if(enemyHealth <= 0)
        {
            isDead = true;
        }
    }

    public bool getIsDead()
    {
        return isDead;
    }

    public int getHealth()
    {
        return enemyHealth;
    }

    public int getMaxHealth()
    {
        return enemyMaxHealth;
    }

    public int getStrength()
    {
        return enemyStrength;
    }

    public void setHealth(int health)
    {
        this.enemyHealth = health;

        if (enemyHealth > 0)
        {
            isDead = false;
        }
    }

    public void setStrength(int strength)
    {
        this.enemyStrength = strength;
    }
}
