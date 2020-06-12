using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int health;
    public GameObject damageSound;
    public AnimationClip Death;
    public Animator animator;

    private void Update()
    {
        if (health <= 0)
        {
            Dead();
        }
    }

    public void TakeDamage(int damage)
    {
        damageSound.GetComponent<AudioSource>().Play();
        health -= damage;
        Debug.Log("damage TAKEN");
    }

    public void Dead()
    {
        //GetComponent<Animation>().Play(animDie.name);
        animator.SetBool("Death", true);
        Destroy(this.gameObject, Death.length);
    }

}
