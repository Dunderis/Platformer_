using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 10;
    public float jumpHeight = 3;
    
    [Header("Jump")]
    public Transform groundCheck; //player legs
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    
    [Header("Jump Mechanics")]
    public float coyoteTime = 0.3f;
    public int maxJumps = 2;
    private int currentJumps = 1;
    
    public float jumpBufferTime = 0.2f;
    
    
    private float jumpBufferCounter;
    private float coyoteTimeCounter;
    private Rigidbody2D _rigidbody2D;
    private bool isGrounded;
    private Rigidbody2D rb;
    private float inputX;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        if (Input.GetButtonDown("Dash"))
        {
            movementSpeed *= 10;
            rb.velocity = new Vector2(inputX * movementSpeed, rb.velocity.y);
            
        }
        else
        {
            movementSpeed  = 10;
        }
        

        if (currentJumps < maxJumps && Input.GetButtonDown("Jump"))
        {
            currentJumps++;
            if (Input.GetButtonDown("Jump"))
            {
                jumpBufferCounter = jumpBufferTime;
            }
            else
            {
                jumpBufferCounter -= Time.deltaTime;
            }

            if (coyoteTimeCounter > 0 && currentJumps== maxJumps-1)
            {
                if (coyoteTimeCounter > 0 && jumpBufferCounter > 0)
                {
                    jumpBufferCounter = 0;
                    var jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y * rb.gravityScale);
            
                    rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
                }
            }
            
        }
        else
        {
            if (isGrounded)
            {
                currentJumps = 0;
            }
        }
        
    }

    private void FixedUpdate()
    {
        
        rb.velocity = new Vector2(inputX * movementSpeed, rb.velocity.y);
        
    }
}
