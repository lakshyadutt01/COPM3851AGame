using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCollectible : MonoBehaviour
{
    public AudioSource collectible;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null) // Checks to see if you're not an Enemy
        {
            controller.ChangeJump(1); //Gives you +1 Jump
            collectible.Play();
            Destroy(gameObject); // Then Destroys itself

        }
    }


}
