using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float Speed;
    Rigidbody2D rigidbody2d;

    public int enemyDamage;

    private Transform player;
    private float distance;
    public float aggroRange;
    public float stopSwitching;
    bool facingRight;

    public Animator animator;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        distance = Vector3.Distance(player.position, transform.position);

        if (IsFacingRight())
        {
            rigidbody2d.velocity = new Vector2(Speed, 0f);
        }
        else
        {
            rigidbody2d.velocity = new Vector2(-Speed, 0f);
        }

        if (distance <= aggroRange)
        {
            if (distance >= stopSwitching)
            {
                Speed = 0;
                animator.SetBool("Defense", true);
                if (player.position.x > transform.position.x && !facingRight) //if the target is to the right of enemy and the enemy is not facing right
                    Flip();
                if (player.position.x < transform.position.x && facingRight)
                    Flip();
            }
        }

        if (distance >= aggroRange)
        {
            Speed = 2;
            animator.SetBool("Defense", false);
        }

    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rigidbody2d.velocity.x)), transform.localScale.y);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-enemyDamage);
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }

}