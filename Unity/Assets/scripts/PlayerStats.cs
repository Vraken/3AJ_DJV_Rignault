using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    private int playerHealth;
    private bool isDead;

	// Use this for initialization
	void Start () {
        playerHealth = 100;
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

    public bool getisDead()
    {
        return isDead;
    }

    void OnTriggerEnter(Collider weapon)
    {

    }
}
