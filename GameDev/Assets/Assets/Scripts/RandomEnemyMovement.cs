using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class RandomEnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f; // Speed of movement
    
    public Rigidbody2D rb; // Rigidbody component for physics interactions  
    public Vector2 v; // Velocity vector for Rigidbody

    private int direction = 1; // 1 = right, -1 = left

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        v.x = moveSpeed; // Set initial velocity
        v.y = 0; // No vertical movement
        rb.velocity = v; // Apply velocity to Rigidbody
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        // string name = obj.gameObject.name;
        if (obj.gameObject.CompareTag("obstacle"))
        {
            v.x = -v.x; // Reverse horizontal velocity
            rb.velocity = v; // Update Rigidbody velocity
        }
    }

}