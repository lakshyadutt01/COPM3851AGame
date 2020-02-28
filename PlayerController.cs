using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;

    public int maxHealth = 3;
    public int currentHealth;
    public int health {  get { return currentHealth; } }
    public GameObject Heart1, Heart2, Heart3, EmptyHeart1, EmptyHeart2, EmptyHeart3;

    public float jumpVelocity = 7f;
    public float moveSpeed = 10f;
    public int movingLeft;

    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    private int extraDash;
    public int extraDashValue;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue;

    void Start()
    {

        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();

        currentHealth = maxHealth;
        EmptyHeart1.gameObject.SetActive(false);
        EmptyHeart2.gameObject.SetActive(false);
        EmptyHeart3.gameObject.SetActive(false);

        extraJumps = extraJumpsValue;

        extraDash = extraDashValue;
        dashTime = startDashTime;

    }



    void Update()
    {

        switch (currentHealth)
        {
            case 3:
                Heart1.gameObject.SetActive(true);
                EmptyHeart1.gameObject.SetActive(false);
                Heart2.gameObject.SetActive(true);
                EmptyHeart2.gameObject.SetActive(false);
                Heart3.gameObject.SetActive(true);
                EmptyHeart3.gameObject.SetActive(false);
                break;
            case 2:
                Heart1.gameObject.SetActive(true);
                EmptyHeart1.gameObject.SetActive(false);
                Heart2.gameObject.SetActive(true);
                EmptyHeart2.gameObject.SetActive(false);
                Heart3.gameObject.SetActive(false);
                EmptyHeart3.gameObject.SetActive(true);
                break;
            case 1:
                Heart1.gameObject.SetActive(true);
                EmptyHeart1.gameObject.SetActive(false);
                Heart2.gameObject.SetActive(false);
                EmptyHeart2.gameObject.SetActive(true);
                Heart3.gameObject.SetActive(false);
                EmptyHeart3.gameObject.SetActive(true);
                break;
            case 0:
                Heart1.gameObject.SetActive(false);
                EmptyHeart1.gameObject.SetActive(true);
                Heart2.gameObject.SetActive(false);
                EmptyHeart2.gameObject.SetActive(true);
                Heart3.gameObject.SetActive(false);
                EmptyHeart3.gameObject.SetActive(true);
                break;

                // Depending on the current value of the players health, the sprites will show up or hide themselves
        }

        if (currentHealth == 0)
        {
            Destroy(gameObject); // If you die, the player disappears
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround); //Checking if player is grounded

        Movement();

        if(direction == 0)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift) && extraDash > 0)
            {
                extraDash--;

                if(movingLeft < 0)
                {
                    direction = 1;
                }
                else if (movingLeft > 0)
                {
                    direction = 2;
                }

                // Depending on direction facing and if they have dash available, will move the player quickly in one direction
            }
        }
        else
        {
            if(dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rigidbody2d.velocity = Vector2.zero;
                // Sets the dash timer
            }
            else
            {
                dashTime -= Time.deltaTime;

                if(direction == 1)
                {
                    rigidbody2d.velocity = Vector2.left * dashSpeed;
                }
                else if (direction == 2)
                {
                    rigidbody2d.velocity = Vector2.right * dashSpeed;                  
                }

                //Code for moving the player in the direction facing
            }
        }

        if(isGrounded == true)
        {
            extraJumps = extraJumpsValue;
            extraDash = extraDashValue;
            // When grounded, returns the values to their original.
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
            extraJumps--;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
        }
        // Checks for space input and makes sure you can jump again.

    }

   
    private void Movement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody2d.velocity = new Vector2(-moveSpeed, rigidbody2d.velocity.y);
            movingLeft = -1;
        }
        // When the user inputs the left arrow, it moves the character left. And also assigns the variable leftArrow to help the animator
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rigidbody2d.velocity = new Vector2(+moveSpeed, rigidbody2d.velocity.y);
                movingLeft = 1;
            }
            // Just like before, except you moved right
            else
            {
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
                movingLeft = 0;
            }
            // This is in case theres no input so nothing moves
        }
    }

    public void ChangeHealth(int amount) //changes your health by an amount
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth); //Does the math to calculate the Health

        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
