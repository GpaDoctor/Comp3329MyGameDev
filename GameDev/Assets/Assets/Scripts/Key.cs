using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// using UnityEngine;

public class Key : MonoBehaviour
{
    public enum KeyColor { Red, Blue, Green, Yellow } // Color identifier
    
    [Header("Key Settings")]
    public KeyColor color; // Set this in the Inspector per prefab
    
    [Header("Visuals")]
    [SerializeField] private ParticleSystem pickupEffect;
    [HideInInspector] public bool isCollected = false;

    public void PickUp(Transform holder)
    {
        if (isCollected) return;
        
        isCollected = true;
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<Collider2D>());
        
        transform.SetParent(holder);
        transform.localPosition = Vector3.zero;
        
        // if (pickupEffect != null)
        //     Instantiate(pickupEffect, transform.position, Quaternion.identity);
    }
    // void Update()
    // {
    //     if (transform.position.y < -10f)
    //         Destroy(gameObject);
    // }
}