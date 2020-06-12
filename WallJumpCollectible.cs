using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpCollectible : MonoBehaviour
{
    public AudioSource collectible;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null) // Checks to see if you're not an Enemy
        {
            controller.ChangeWall(1); //Gives you +1 WallJump
            collectible.Play();
            Destroy(gameObject); // Then Destroys itself

        }
    }
}