using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 6;
    public int currentHealth;

    void Awake() {
        currentHealth = startingHealth;
    }

    public void DamageHealth(int amount) {
        currentHealth -= amount;
        if (currentHealth <= 0) {
            currentHealth = 0;
            Debug.Log("Player is dead");
        }
    }
}
