using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int health;
    public GameObject damageSound;

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        damageSound.GetComponent<AudioSource>().Play();
        health -= damage;
        Debug.Log("damage TAKEN");
    }

}
