using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// public class pickupController : MonoBehaviour
// {
//     [Header("Key Holding")]
//     public Transform keyHoldPosition; // Empty child object on cat
    
//     private Key currentKey = null;

//     void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Key") && currentKey == null)
//         {
//             Key key = other.GetComponent<Key>();
//             if (key != null && !key.isCollected)
//             {
//                 currentKey = key;
//                 key.PickUp(keyHoldPosition);
//             }
//         }

//         // Lock Interaction
//         if (currentKey != null && other.CompareTag("Lock"))
//         {
//             if (other.TryGetComponent<Lock>(out var lockObj) && lockObj.TryUnlock(currentKey.color))
//             {
//                 Destroy(currentKey.gameObject);
//                 currentKey = null;
//             }
//         }

//     }

//     // Visual debug
//     void OnDrawGizmosSelected()
//     {
//         if (keyHoldPosition != null)
//         {
//             Gizmos.color = Color.yellow;
//             Gizmos.DrawWireSphere(keyHoldPosition.position, 0.2f);
//         }
//     }
// }


public class PlayerKeyController : MonoBehaviour
{
    public static System.Action OnKeyPickedUp;
    public static System.Action OnKeyUsed;
    
    private Key currentKey = null;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Key Pickup
        if (other.CompareTag("Key") && currentKey == null)
        {
            currentKey = other.GetComponent<Key>();
            if (currentKey != null)
            {
                currentKey.PickUp(transform);
                OnKeyPickedUp?.Invoke(); // Notify spawner
            }
        }
        
        // Lock Interaction
        if (other.CompareTag("Lock") && currentKey != null)
        {
            Lock lockObj = other.GetComponent<Lock>();
            if (lockObj != null && lockObj.TryUnlock(currentKey.color))
            {
                Destroy(currentKey.gameObject);
                currentKey = null;
                OnKeyUsed?.Invoke(); // Notify spawner
            }
        }
    }
}