using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

    private int enemyHealth;
    private float enemyStamina;
    private int enemyStrength;

    private bool isDead;

	// Use this for initialization
	void Start () {
        enemyHealth = 100;
        enemyStrength = 2;
        isDead = false;
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

    public int getStrength()
    {
        return enemyStrength;
    }

    public void setHealth(int health)
    {
        this.enemyHealth = health;
    }

    public void setStrength(int strength)
    {
        this.enemyStrength = strength;
    }
}
