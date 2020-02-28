using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCollectible : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null) // Checks to see if you're not an Enemy
        {
            controller.extraJumpsValue++; //Gives you +1 Jump
            Destroy(gameObject); // Then Destroys itself

        }
    }


}
