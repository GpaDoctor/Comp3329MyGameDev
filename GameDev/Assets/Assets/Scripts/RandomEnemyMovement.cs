using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RandomEnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;          // Speed of movement
    public float directionChangeInterval = 2f; // Time between direction changes
    public float minX = -5f;              // Left boundary
    public float maxX = 5f;               // Right boundary

    private float timer;
    private int direction = 1;            // 1 = right, -1 = left

    void Start()
    {
        // Start with a random direction
        direction = Random.Range(0, 2) * 2 - 1; // Returns -1 or 1
        timer = directionChangeInterval;
    }

    void Update()
    {
        // Move the enemy
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);

        // Random direction change logic
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            direction = Random.Range(0, 2) * 2 - 1; // Randomly pick -1 or 1
            timer = directionChangeInterval;
        }

        // Boundary check (optional)
        if (transform.position.x <= minX || transform.position.x >= maxX)
        {
            direction *= -1; // Reverse direction at boundaries
        }
    }
}