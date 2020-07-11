﻿using System.Collections;
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
    public float lowJumpMultiplier = 2f;

    public SpriteRenderer spriteRenderer;

    public float rememberGroundedFor;
    float lastTimeGrounded;

    private bool facingRight = true;
    private float lastMovement;

    public Animator animator;

    float travel = 0f;
    float currentheight;
    float previousheight;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if((facingRight == true && lastMovement == -1) || (facingRight == false && lastMovement == 1))
        {
            spriteRenderer.flipX = true;
        }
        transform.rotation = new Quaternion(0, 0, 0, 0);
        Move();
        Jump();
        BetterJump();
        //CheckIfGrounded();
        CheckIfDead();
        CheckIfFalling();
        print(isGrounded);
       // AnimateJump();
    }

    void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)||  Input.GetKeyDown(KeyCode.W)) && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor))
        {
            animator.SetBool("pressedJump", true);
            isGrounded = false;
            animator.SetBool("isGrounded", false);
            Invoke("JumpEffect", 0.05f);
        }
    }

    void JumpEffect()
    {
        animator.SetBool("Jumping", true);
        animator.SetBool("pressedJump", false);
        animator.SetBool("isGrounded", false);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
            animator.SetBool("isFalling", true);
            animator.SetBool("Jumping", false);
        }
        previousheight = currentheight;
    }

    void Move()
    {

        float x = Input.GetAxisRaw("Horizontal");

        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(moveBy));

        lastMovement = x;

        if(x > 0 && !facingRight)
        {
            Flip();
        } else if (x < 0 && facingRight)
        {
            Flip();
        }
    }

    //void CheckIfGrounded()
    //{
    //    //Collider2D colliders = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
    //    //if (colliders != null)
    //    //{
    //    //    isGrounded = true;
    //    //    animator.SetBool("isGrounded", true);
    //    //    animator.SetBool("Jumping", false);
    //    //}
    //    //else
    //    //{
    //    //    if (isGrounded)
    //    //    {
    //    //        previousheight = transform.position.y;
    //    //        lastTimeGrounded = Time.time;
    //    //    }
    //    //    isGrounded = false;
    //    //}
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            animator.SetBool("isGrounded", true);
            animator.SetBool("Jumping", false);
        } else
        {
            isGrounded = false;
        }
    }

    void BetterJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void CheckIfDead()
    {
        if (transform.position.y < fallThreshold)
        {
            GameState.isGameOver = true;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}