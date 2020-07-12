using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce;
    public float speed;
    public float runSpeed;
    public float fallThreshold = -5;
    Rigidbody2D rb;

    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;

    public float fallMultiplier = 2.5f;
    public Vector2 lowJumpVector;

    public SpriteRenderer spriteRenderer;

    public float rememberGroundedFor;
    float lastTimeGrounded;

    private bool facingRight = true;
    private float lastMovement;

    public Animator animator;

    float travel = 0f;
    float currentheight;
    float previousheight;

    public bool jumpKeyHeld;
    public bool isJumping = false;

    public GameObject glowObject;

    public static bool isHittingWall = false;

    public AudioSource walkAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        isHittingWall = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        walkAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] groundTile = GameObject.FindGameObjectsWithTag("GroundTile");
        foreach(GameObject tile in groundTile)
        {
            isHittingWall = isHittingWall || tile.GetComponent<WallBehavior>().isHittingWall;
        }
        if (!PauseMenuScript.isGamePaused)
        {
            if ((facingRight == true && lastMovement == -1) || (facingRight == false && lastMovement == 1))
            {
                spriteRenderer.flipX = true;
            }
            transform.rotation = new Quaternion(0, 0, 0, 0);
            Move();
            //Jump();
            Jump();
            CheckIfGrounded();
            CheckIfDead();
            CheckIfFalling();

            if (GameState.isGameOver || GameState.isLevelWon || !Input.anyKey)
            {
                walkAudioSource.Stop();
            }
            else if (isGrounded == true && rb.velocity.magnitude > 2f && walkAudioSource.isPlaying == false)
            {
                walkAudioSource.Play();
            }
            if (isGrounded == true && rb.velocity.magnitude < 0.5f && walkAudioSource.isPlaying == true || !isGrounded)
            {
                walkAudioSource.Stop();
            }
        }
    }

    //void Jump()
    //{
    //    if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)||  Input.GetKeyDown(KeyCode.W)) && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
    //    {
    //        animator.SetBool("Jumping", false);
    //        animator.SetBool("isFalling", false);
    //        animator.SetBool("isGrounded", false);
    //        animator.SetBool("pressedJump", true);
    //        isGrounded = false;
    //        BetterJump();
    //        SoundManagerScript.PlaySound("Jump");
    //        Invoke("JumpEffect", 0.05f);
    //    }
    //}

    void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) 
        {

            jumpKeyHeld = true;
            if (isGrounded)
            {
                isJumping = true;
                animator.SetBool("Jumping", false);
                animator.SetBool("isFalling", false);
                animator.SetBool("isGrounded", false);
                animator.SetBool("pressedJump", true);
                isGrounded = false;
                SoundManagerScript.PlaySound("Jump");
                Invoke("JumpEffect", 0.3f);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        } else if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            Debug.Log("key released");
            jumpKeyHeld = false;
            Debug.Log("JKH after release: " + jumpKeyHeld);
        }

        if (isJumping)
        {
            if (!jumpKeyHeld)
            {
                rb.AddForce(lowJumpVector);
            }
        }
    }

    void JumpEffect()
    {
        animator.SetBool("pressedJump", false);
        animator.SetBool("isGrounded", false);
        animator.SetBool("Jumping", true);
        //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void CheckIfFalling()
    {
        currentheight = transform.position.y;
        travel = currentheight - previousheight;
        if (travel > 0.0 && !isGrounded)
        {
            animator.SetBool("isGrounded", false);
            animator.SetBool("isFalling", false);
            animator.SetBool("Jumping", true);
        } else if (travel < 0.0 && !isGrounded)
        {
            animator.SetBool("isGrounded", false);
            animator.SetBool("Jumping", false);
            animator.SetBool("isFalling", true);
        } else if (travel == 0 && isGrounded)
        {
            animator.SetBool("isGrounded", true);
            animator.SetBool("Jumping", true);
            animator.SetBool("isFalling", false);
        }
        previousheight = currentheight;
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        if (SwitchController.glowState)
        {
            moveBy = x * speed + 500;
        }
        
        if (!isHittingWall)
        {
            rb.velocity = new Vector2(moveBy, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(moveBy));

            lastMovement = x;

            if (x > 0 && !facingRight)
            {
                Flip();
            }
            else if (x < 0 && facingRight)
            {
                Flip();
            }
        }
        if (facingRight && x < 0)
        {
            rb.velocity = new Vector2(moveBy, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(moveBy));

            lastMovement = x;
            Flip();
        }
        else if (!facingRight && x > 0)
        {
            rb.velocity = new Vector2(moveBy, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(moveBy));

            lastMovement = x;
            Flip();
        }
    }

    void CheckIfGrounded()
    {
        Collider2D colliders = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
        if (colliders != null)
        {
            isGrounded = true;
            animator.SetBool("isGrounded", true);
            animator.SetBool("Jumping", false);
        }
        else
        {
            if (isGrounded)
            {
                previousheight = transform.position.y;
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        isGrounded = true;
    //        animator.SetBool("isGrounded", true);
    //        animator.SetBool("Jumping", false);
    //    } else
    //    {
    //        isGrounded = false;
    //    }
    //}

    

    void CheckIfDead()
    {
        
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerDie")
        {
            GameState.isGameOver = true;
        }

        if(collision.tag == "SceneSwitcher")
        {

            //SwitchController.CalculateRandomTime(true);
        }

        if(collision.tag == "SceneSwitcherRight")
        {
                //SwitchController.CalculateRandomTime(true);
            
        }
        if (collision.tag == "SceneSwitcherLeft")
        {
            if (!facingRight)
            {
                //SwitchController.CalculateRandomTime(true);
            }
        }
    }

    public void StopMovingSound()
    {
        walkAudioSource.Stop();
    }
}