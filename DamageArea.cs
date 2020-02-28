using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    void OnTriggerStay2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();
        //  Grabs the Controller

        if (controller != null) // Checks if its the Player
        {
            controller.ChangeHealth(-1); // Deducts the health
            player.transform.position = respawnPoint.transform.position;
        }
        // If the player collides with the invisible wall on the bottom screen or the spikes, puts them back at the respawn point and deducts a health point

    }

}
