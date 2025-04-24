using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;



// public class RandomEnemyMovement : MonoBehaviour
// {
//     [Header("Movement Settings")]
//     public float moveSpeed = 3f;           // Speed of movement
//     public float patrolRange = 5f;         // How far left/right the enemy can move from its start position
//     public float directionChangeInterval = 2f; // Time between random direction changes

//     private float timer;
//     private int direction = 1;             // 1 = right, -1 = left
//     private float startingX;               // Stores initial X position
//     private float leftBoundary;            // Calculated left boundary
//     private float rightBoundary;           // Calculated right boundary

//     void Start()
//     {
//         startingX = transform.position.x;
//         leftBoundary = startingX - patrolRange;
//         rightBoundary = startingX + patrolRange;
        
//         // Initialize with random direction
//         direction = Random.Range(0, 2) * 2 - 1; // Returns -1 or 1
//         timer = directionChangeInterval;
//     }

//     void Update()
//     {
//         // Move the enemy
//         transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);

//         // Random direction change
//         timer -= Time.deltaTime;
//         if (timer <= 0)
//         {
//             direction = Random.Range(0, 2) * 2 - 1;
//             timer = directionChangeInterval;
//         }

//         // Boundary check (relative to starting position)
//         if (transform.position.x <= leftBoundary || transform.position.x >= rightBoundary)
//         {
//             direction *= -1; // Reverse direction
//         }
//     }

//     // Visualize patrol range in Scene view
//     void OnDrawGizmosSelected()
//     {
//         if (!Application.isPlaying)
//         {
//             startingX = transform.position.x;
//             leftBoundary = startingX - patrolRange;
//             rightBoundary = startingX + patrolRange;
//         }

//         Gizmos.color = Color.cyan;
//         Gizmos.DrawLine(new Vector3(leftBoundary, transform.position.y - 0.5f, 0), 
//                         new Vector3(leftBoundary, transform.position.y + 0.5f, 0));
//         Gizmos.DrawLine(new Vector3(rightBoundary, transform.position.y - 0.5f, 0), 
//                         new Vector3(rightBoundary, transform.position.y + 0.5f, 0));
//         Gizmos.DrawLine(new Vector3(leftBoundary, transform.position.y, 0), 
//                         new Vector3(rightBoundary, transform.position.y, 0));
//     }
// }

// using UnityEngine;

public class RandomEnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float patrolRange = 5f;
    public float directionChangeInterval = 2f;

    private float timer;
    private int direction = 1;
    private float startingX;
    private float leftBoundary;
    private float rightBoundary;

    void Start()
    {
        startingX = transform.position.x;
        leftBoundary = startingX - patrolRange;
        rightBoundary = startingX + patrolRange;
        
        direction = Random.Range(0, 2) * 2 - 1;
        timer = directionChangeInterval;
    }

    void Update()
    {
        // Movement
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);

        // Random direction change timer
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            direction = Random.Range(0, 2) * 2 - 1;
            timer = directionChangeInterval;
        }

        // Boundary check
        if (transform.position.x <= leftBoundary || transform.position.x >= rightBoundary)
        {
            direction *= -1;
        }
    }

    // New: Handle collisions with obstacles
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("obstacle")) // Ignore player collisions
        {
            direction *= -1; // Reverse direction on collision
        }
    }

    // Visualize patrol range (optional)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 startPos = Application.isPlaying ? new Vector3(startingX, transform.position.y, 0) : transform.position;
        Gizmos.DrawLine(new Vector3(startPos.x - patrolRange, transform.position.y - 0.5f, 0), 
                        new Vector3(startPos.x - patrolRange, transform.position.y + 0.5f, 0));
        Gizmos.DrawLine(new Vector3(startPos.x + patrolRange, transform.position.y - 0.5f, 0), 
                        new Vector3(startPos.x + patrolRange, transform.position.y + 0.5f, 0));
    }
}