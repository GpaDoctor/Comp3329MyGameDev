using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// using UnityEngine;

public class pickupController : MonoBehaviour
{
    [Header("Key Holding")]
    public Transform keyHoldPosition; // Empty child object on cat
    
    private Key currentKey = null;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key") && currentKey == null)
        {
            Key key = other.GetComponent<Key>();
            if (key != null && !key.isCollected)
            {
                currentKey = key;
                key.PickUp(keyHoldPosition);
            }
        }

        // Lock Interaction
        if (currentKey != null && other.CompareTag("Lock"))
        {
            if (other.TryGetComponent<Lock>(out var lockObj) && lockObj.TryUnlock(currentKey.color))
            {
                Destroy(currentKey.gameObject);
                currentKey = null;
            }
        }

    }

    // Visual debug
    void OnDrawGizmosSelected()
    {
        if (keyHoldPosition != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(keyHoldPosition.position, 0.2f);
        }
    }
}