using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// using UnityEngine;

// public class DestroyWhenOutOfBounds : MonoBehaviour
// {
//     private float boundaryY;

//     public void SetBoundary(float yPosition)
//     {
//         boundaryY = yPosition;
//     }

//     void Update()
//     {
//         if (transform.position.y < boundaryY)
//         {
//             Destroy(gameObject);
//         }
//     }
// }


// using UnityEngine;

public class DestroyWhenOutOfBounds : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Y position where objects get destroyed")]
    [SerializeField] private float boundaryY = -10f;
    
    [Tooltip("Get boundary from spawner automatically")]
    [SerializeField] private bool useSpawnerBoundary = true;

    public void Initialize(float newBoundary)
    {
        boundaryY = newBoundary;
    }

    void Start()
    {
        if (useSpawnerBoundary)
        {
            var spawner = FindObjectOfType<EnemySpawner>();
            if (spawner != null)
            {
                boundaryY = spawner.DestroyY; // Now matches the property name
            }
        }
    }

    void Update()
    {
        if (transform.position.y < boundaryY)
        {
            Destroy(gameObject);
        }
    }

    #if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            new Vector3(-100, boundaryY, 0),
            new Vector3(100, boundaryY, 0)
        );
    }
    #endif
}