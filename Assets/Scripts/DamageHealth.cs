using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHealth : MonoBehaviour
{
public int healthDamage = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null) {
                playerHealth.DamageHealth(healthDamage);
            }
        }
    }
}
