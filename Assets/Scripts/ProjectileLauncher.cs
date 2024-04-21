using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField]
    private Rigidbody projectileRigidBody; // class level variable
    [SerializeField]
    private float projectilePower = 1500; // class level variable
    [SerializeField]
    private GameObject muzzle;

    [SerializeField]
    private float COOLDOWN_TIME = 0.5f; // how quickly the player can fire a projectile: in 0.5 seconds
    private float coolDown = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDown <= 0) // check if the cooldown counter is equal to zero or less than zero
        {
            if (Input.GetButtonUp("Fire1")) // check if the Fire1 button has been pressed.
            {
                coolDown = COOLDOWN_TIME;

                // Instantiate the projectile.
                Rigidbody aInstance = Instantiate(projectileRigidBody, muzzle.transform.position, transform.rotation) as Rigidbody;
                
                // Add force.
                Vector3 forward = transform.TransformDirection(Vector3.forward);
                aInstance.AddForce(forward * projectilePower);

                // Destroy the object after X seconds.
                Destroy(aInstance.gameObject, 8);
            }
        }
        else
        {
            coolDown = coolDown - Time.deltaTime;
        }
    }
}

// If this is true, we create an instance of the project using the Rigidbody component variable,
// then add a force to it to make it move. Finally, we tell it to destroy itself in 8 seconds.
// If the cooldown counter is not equal to zero or less than zero, we decrease it by the
// amount of time since the update method was last called.