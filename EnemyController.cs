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
            Speed = 0;
            animator.SetBool("Defense", true);
            //Vector2 direction = player.position - transform.position;
            //float angle = Mathf.Atan(direction.x) * Mathf.Rad2Deg;
            //rigidbody2d.rotation = angle;
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
    
}