using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float speed = 5f;
    public float fallMultiplier = 2.5f;
    public float jumpPower = 50f;
    public float jumpTime = 0.35f;
    public float distanceGround = 0.1f;

    [Header("References")]
    public LayerMask groundLayers;

    private Rigidbody2D body;
    private BoxCollider2D boxCollider2d;
    private float vertical, horizontal;
    public bool isGrounded = false;

    // Jumping 
    private float jumpTimeCounter;
    private bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");

        jump();
    }

    void FixedUpdate(){
        checkIsGrounded();
        move();
        betterFalling();
    }

    // Allow movement
    private void move(){
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

    // Check if grounded
    private void checkIsGrounded(){
        if (Physics2D.Raycast (boxCollider2d.bounds.center, Vector2.down, boxCollider2d.bounds.extents.y + distanceGround, groundLayers)){
            isGrounded = true;
        }else 
            isGrounded = false;
    }
}
