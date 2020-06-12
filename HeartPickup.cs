using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    public AudioSource healthSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>(); //Grabs the Player controller

        if (controller != null) // Checks to see if you're not an Enemy
        {
            if (controller.health < controller.maxHealth) //checks if you're full health
            {
                controller.ChangeHealth(1); //Gives you +1 HP
                healthSound.Play();
                Destroy(gameObject); // Then Destroys itself
            }
        }
    }


}
