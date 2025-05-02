using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// using UnityEngine;

public class DestroyWhenOutOfBounds : MonoBehaviour
{
    private float boundaryY;

    public void SetBoundary(float yPosition)
    {
        boundaryY = yPosition;
    }

    void Update()
    {
        if (transform.position.y < boundaryY)
        {
            Destroy(gameObject);
        }
    }
}