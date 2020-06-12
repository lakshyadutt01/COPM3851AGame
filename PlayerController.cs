using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;
    private Renderer playerSprite;
    public Animator animator;
    public Gameover Gameover;

    public int maxHealth = 3;
    public int currentHealth;
    public int health {  get { return currentHealth; } }
    public GameObject Heart1, Heart2, Heart3, EmptyHeart1, EmptyHeart2, EmptyHeart3;
    public AudioSource playerHit;
    public AudioSource lowHealth;
    public AudioSource deathSound;
    private float timeBtwLowHealth;
    public float startTimeBtwLowHealth;
    public float timeInvincible;
    private bool isInvincible;
    private float invincibleTimer;

    public float jumpVelocity = 7f;
    public float moveSpeed = 10f;
    public int movingLeft;

    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    private int extraDash;
    public int extraDashValue;
    public int dash { get { return extraDashValue; } }
    public int maxDashs = 5;
    public AudioSource dashSound;
    public AudioClip[] dashSoundsCollection;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private bool isWalled;
    public Transform wallCheck;
    public float checkWallRadius;
    public LayerMask whatIsWall;
    private int extraWallJump;
    public int extraWallJumpValue;
    public int wall { get { return extraWallJumpValue; } }
    public int maxWallJumps = 5;
    public AudioSource wallJumpSound;

    public int maxJumps = 5;
    private int extraJumps;
    public int extraJumpsValue;
    public int jump { get { return extraJumpsValue; } }
    public AudioSource jumpSound;
    public AudioClip[] jumpSoundsCollection;
    public AudioSource doubleJumpSound;
    public AudioClip[] doubleJumpSoundsCollection;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        playerSprite = GetComponent<Renderer>();

        currentHealth = PlayerPrefs.GetInt("CurrentHealth");
        extraJumpsValue = PlayerPrefs.GetInt("ExtraJumpsValue");
        extraDashValue = PlayerPrefs.GetInt("ExtraDashValue");
        extraWallJumpValue = PlayerPrefs.GetInt("ExtraWallJumpValue");
        EmptyHeart1.gameObject.SetActive(false);
        EmptyHeart2.gameObject.SetActive(false);
        EmptyHeart3.gameObject.SetActive(false);

        extraJumps = extraJumpsValue;
        extraWallJump = extraWallJumpValue;
        jumpSound = GetComponent<AudioSource>();
        doubleJumpSound = GetComponent<AudioSource>();

        extraDash = extraDashValue;
        dashTime = startDashTime;
        dashSound = GetComponent<AudioSource>();


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
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isWalled = Physics2D.OverlapCircle(wallCheck.position, checkWallRadius, whatIsWall);

        Movement();

        if (isInvincible)
        {
            animator.SetBool("Invincible", true);
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                animator.SetBool("Invincible", false);
                isInvincible = false;
            }
        }

        if (direction == 0)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift) && extraDash > 0)
            {

                if(movingLeft < 0)
                {
                    direction = 1;
                    extraDash--;
                }
                else if (movingLeft > 0)
                {
                    direction = 2;
                    extraDash--;
                }
            }
        }
        else
        {
            if(dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rigidbody2d.velocity = Vector2.zero;
                dashSound.clip = dashSoundsCollection[Random.Range(0, dashSoundsCollection.Length)];
                dashSound.PlayOneShot(dashSound.clip);
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
            }
        }

        if(isGrounded == true)
        {
            extraJumps = extraJumpsValue;
            extraDash = extraDashValue;
            extraWallJump = extraWallJumpValue;
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0 | isWalled == true)
        {
            if (isWalled == true && extraWallJump > 0)
            {
                rigidbody2d.velocity = Vector2.up * jumpVelocity;
                extraWallJump--;
                wallJumpSound.Play();
            }
            else if (isWalled == false | extraJumps > 0 && isGrounded == false)
            {
                doubleJumpSound.clip = doubleJumpSoundsCollection[Random.Range(0, doubleJumpSoundsCollection.Length)];
                rigidbody2d.velocity = Vector2.up * jumpVelocity;
                extraJumps--;
                doubleJumpSound.PlayOneShot(doubleJumpSound.clip);
            }
        }
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            jumpSound.clip = jumpSoundsCollection[Random.Range(0, jumpSoundsCollection.Length)];
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
            jumpSound.PlayOneShot(jumpSound.clip);
        }

        if (currentHealth == 1 && timeBtwLowHealth <= 0)
        {
            lowHealth.GetComponent<AudioSource>().Play();
            timeBtwLowHealth = startTimeBtwLowHealth;
        }
        else
        {
            timeBtwLowHealth -= Time.deltaTime;
        }

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
                animator.SetBool("Running", false);
                rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
                movingLeft = 0;
            }
            // This is in case theres no input so nothing moves
        }
        if(movingLeft == 1 || movingLeft == -1)
        {
            animator.SetBool("Running", true);
        }
        if(movingLeft == 1)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (movingLeft == -1)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    //public void Restart()
    //{
    //    Debug.Log("wow howq buggy lmao");
    //    SceneManager.LoadScene(0);
    //}

    public void ChangeHealth(int amount) //changes your health by an amount
    {

        if (amount < 0)
        {
            if (isInvincible) //checks if invincible, if so it returns
                return;

            isInvincible = true; //sets you to invincible
            invincibleTimer = timeInvincible; //gives you the timer of invincible
            playerHit.GetComponent<AudioSource>().Play();
        }

            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth); //Does the math to calculate the Health

            PlayerPrefs.SetInt("CurrentHealth", currentHealth);
            Debug.Log(currentHealth + "/" + maxHealth);

            if (currentHealth == 0)
            {
                deathSound.Play();
                playerSprite.enabled = false;
                Destroy(rigidbody2d);
                boxCollider2d.enabled = false;
                Gameover.DeathMenu();
            }
        

    }

    public void ChangeJump(int amount)
    {
        extraJumpsValue = Mathf.Clamp(extraJumpsValue + amount, 0, maxJumps);
        PlayerPrefs.SetInt("ExtraJumpsValue", extraJumpsValue);
    }

    public void ChangeDash(int amount)
    {
        extraDashValue = Mathf.Clamp(extraDashValue + amount, 0, maxDashs);
        PlayerPrefs.SetInt("ExtraDashValue", extraDashValue);
    }

    public void ChangeWall(int amount)
    {
        extraWallJumpValue = Mathf.Clamp(extraWallJumpValue + amount, 0, maxWallJumps);
        PlayerPrefs.SetInt("ExtraWallJumpValue", extraWallJumpValue);
    }
}
