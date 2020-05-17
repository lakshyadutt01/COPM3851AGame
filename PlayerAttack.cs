using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private float timeBetweenAttack;
    public float startTimeBetweenAttack;

    public Transform attackPos;
    public float attackRange;
    //public Animator animator;
    public LayerMask whatIsEnemies;
    public int damage;
    public AudioSource attackSound;
    public AudioClip[] attackSoundsCollection;

    private void Start()
    {
        attackSound = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Q))
        {
            PlayerController controller = GetComponent<PlayerController>();
            if (controller.movingLeft >= 0)
            {
                if (timeBetweenAttack <= 0)
                {
                    //animator.Play("PlayerAttack");
                    attackSound.clip = attackSoundsCollection[Random.Range(0, attackSoundsCollection.Length)];
                    attackSound.PlayOneShot(attackSound.clip);
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                    }
                    timeBetweenAttack = startTimeBetweenAttack;
                }
            }
            if (controller.movingLeft <= 0)
            {
                if (timeBetweenAttack <= 0)
                {
                    //animator.Play("PlayerAttackLeft");
                    attackSound.Play();
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                    }
                    timeBetweenAttack = startTimeBetweenAttack;
                }
            }


        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
