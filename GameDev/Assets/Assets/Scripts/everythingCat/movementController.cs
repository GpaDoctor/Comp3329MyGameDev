using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class movementController : MonoBehaviour // Renamed to be more descriptive
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private float airControlMultiplier = 0.7f;
    [SerializeField] private float variableJumpHeightMultiplier = 0.5f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(1.8f, 0.3f);
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isFacingRight = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        
        // Apply movement with air control
        float currentSpeed = isGrounded ? moveSpeed : moveSpeed * airControlMultiplier;
        rb.velocity = new Vector2(moveInput * currentSpeed, rb.velocity.y);

        // Flip sprite based on movement direction
        if (moveInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && isFacingRight)
        {
            Flip();
        }
    }

    private void HandleJump()
    {
        // Ground check using capsule
        isGrounded = Physics2D.OverlapCapsule(
            groundCheck.position, 
            groundCheckSize, 
            CapsuleDirection2D.Horizontal, 
            0, 
            groundLayer
        );

        // Normal jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Variable jump height (short tap = smaller jump)
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // Visualize ground check in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
    }

}