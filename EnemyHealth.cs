using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int health;
    public GameObject damageSound;
    //public Animator animator;

    private void Update()
    {
        if (health <= 0)
        {
            //animator.SetBool("IsDead", true);
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
