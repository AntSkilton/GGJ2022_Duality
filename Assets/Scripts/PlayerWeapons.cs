using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Weapon
{
    None,
    Fireball,
}

public class PlayerWeapons : MonoBehaviour
{
    public Weapon activeWeapon = Weapon.None;
    public Vector3 fireOffset;
    public GameObject fireballPrefab;

    void Awake()
    {
        if (!fireballPrefab)
        {
            Debug.Log("Fireball prefab is undefined");
        }

        // Create an Action that binds to the primary action control on all devices.
        var action = new InputAction(binding: "*/{primaryAction}");

        // Have it run your code when the Action is triggered.
        action.performed += _ => Fire();

        // Start listening for control changes.
        action.Enable();
    }

    void Fire()
    {
        switch (activeWeapon)
        {
            case Weapon.Fireball:
                {
                    Instantiate(fireballPrefab, transform.position + fireOffset, transform.rotation);
                }
                break;
        }
    }
}
