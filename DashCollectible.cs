using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCollectible : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null) // Checks to see if you're not an Enemy
        {
            controller.extraDashValue++; //Gives you +1 Dash
            Destroy(gameObject); // Then Destroys itself

        }
    }
}
