using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float speed = 5f;
    public float crouchSpeed = 3f;
    public float fallMultiplier = 2.5f;
    public float jumpPower = 50f;
    public float jumpTime = 0.35f;
    public float distanceGround = 0.1f;

    [Header("References")]
    public LayerMask groundLayers;
    public ParticleSystem dust;

    private PlayerAnimation playerAnimation;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider2d;
    private float vertical, horizontal;
    private bool isGrounded = false;

    // Jumping 
    private float jumpTimeCounter;
    private bool isJumping;

    // Crouching
    private bool isCrouching;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        body = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");

        flipSprite();
        crouch();
        jump();
    }

    void FixedUpdate(){
        checkIsGrounded();
        move();
        betterFalling();
    }

    // Allow movement
    private void move(){
        // Set running animation
        if (horizontal == 0 && isCrouching)
            playerAnimation.SetAnimation("crouch");
        else if (horizontal != 0 && isCrouching)
            playerAnimation.SetAnimation("crouch walk");
        else if (horizontal != 0 && isGrounded)
            playerAnimation.SetAnimation("running");
        else if (!isGrounded)
            playerAnimation.SetAnimation("jump");
        else
            playerAnimation.SetAnimation("idle");

        // Move player
        if (isCrouching)
            body.velocity = new Vector2 (horizontal * crouchSpeed, body.velocity.y);
        else
            body.velocity = new Vector2 (horizontal * speed, body.velocity.y);
    }

    // Better falling physics
    private void betterFalling(){
        if (body.velocity.y < 0){
            body.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    // Allow player to jump
    private void jump(){

        // Small jump
        if (Input.GetKeyDown("space") && isGrounded){
            isJumping = true;
            jumpTimeCounter = jumpTime;
            body.velocity = Vector2.up * jumpPower;
        }

        // Hold jump
        if (Input.GetKey("space") && isJumping){
            if (jumpTimeCounter > 0){
                body.velocity = Vector2.up * jumpPower;
                jumpTimeCounter -= Time.deltaTime;
            }else{
                isJumping = false;
            }
        }

        // Reset jump
        if (Input.GetKeyUp("space")){
            isJumping = false;
        }
    }

    // Allow player to crouch
    private void crouch(){
        if (vertical == -1 || (Physics2D.Raycast (boxCollider2d.bounds.center, Vector2.up, boxCollider2d.bounds.extents.y + (distanceGround*4), groundLayers) && isGrounded))
            isCrouching = true;
        else
            isCrouching = false;
    }

    // Flip sprite on direction change
    private void flipSprite(){
        if (horizontal == 1){
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }else if (horizontal == -1){
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }

    // Check if grounded
    private void checkIsGrounded(){
        if (Physics2D.Raycast (boxCollider2d.bounds.center, Vector2.down, boxCollider2d.bounds.extents.y + distanceGround, groundLayers)){
            if (!isGrounded) CreateDust();

            isGrounded = true;
        }else 
            isGrounded = false;
    }

    // Create dust particles 
    private void CreateDust(){
        dust.Play();
    }
}
