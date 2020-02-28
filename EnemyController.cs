using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //public int health;
    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;
    public int enemyDamage;

    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    void Update()
    {
            timer -= Time.deltaTime;


                if (timer < 0)
                {
                    direction = -direction;
                    timer = changeTime;
                }

                // Setting a timer for enemy to move forwards and back.

                Vector2 position = rigidbody2D.position;

                if (vertical)
                {
                    position.y = position.y + Time.deltaTime * speed * direction;
                }
                else
                {
                    position.x = position.x + Time.deltaTime * speed * direction;
                }

                rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        // Code for enemies colliding with player, dealing damage.

        if (player != null)
        {
            player.ChangeHealth(-enemyDamage);
        }
    }
}