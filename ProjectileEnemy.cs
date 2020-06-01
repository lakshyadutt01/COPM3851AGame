using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : MonoBehaviour
{

    [SerializeField] float Speed;
    Rigidbody2D rigidbody2d;
    private float timeBtwShots;
    public float startTimeBtwShots;

    private float distance;
    public float aggroRange;
    public float stopShooting;
    public AudioSource alertStart;
    public AudioSource alertEnd;

    private Transform player;
    public GameObject projectile;
    public AudioSource projectileSound;
    public Animator animator;


    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;

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
            animator.SetBool("IsAlert", true);
            //alertStart.GetComponent<AudioSource>().Play();

            if (distance >= stopShooting)
            {

                if (timeBtwShots <= 0)
                {
                    projectileSound.Play();
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    timeBtwShots = startTimeBtwShots;
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }
            }
        }

        if(distance >= aggroRange)
        {
            Speed = 2;
            alertEnd.GetComponent<AudioSource>().Play();
            animator.SetBool("IsAlert", false);
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

}
