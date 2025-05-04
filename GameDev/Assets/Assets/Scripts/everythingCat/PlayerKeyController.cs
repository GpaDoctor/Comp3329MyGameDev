using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerKeyController : MonoBehaviour
{
    public static System.Action OnKeyPickedUp;
    public static System.Action OnKeyUsed;

    private Key currentKey = null;

    void Start()
    {
        StartCoroutine(ReattachCarriedKey());
    }

    IEnumerator ReattachCarriedKey()
    {
        // Wait 1 frame to ensure KeyManager's CarriedKey is valid
        yield return null;

        if (KeyManager.Instance != null && KeyManager.Instance.CarriedKey != null)
        {
            currentKey = KeyManager.Instance.CarriedKey;
            currentKey.SetFollowTarget(transform);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key") && currentKey == null)
        {
            currentKey = other.GetComponent<Key>();
            if (currentKey != null)
            {
                currentKey.PickUp(transform);
                KeyManager.Instance.SetKey(currentKey);
                OnKeyPickedUp?.Invoke();
            }
        }

        if (other.CompareTag("Lock") && currentKey != null)
        {
            Lock lockObj = other.GetComponent<Lock>();
            if (lockObj != null && lockObj.TryUnlock(currentKey.color))
            {
                Destroy(currentKey.gameObject);
                KeyManager.Instance.DropKey();
                currentKey = null;
                OnKeyUsed?.Invoke();
            }
        }
    }
}



// using UnityEngine;

// public class PlayerKeyController : MonoBehaviour
// {
//     public static System.Action OnKeyPickedUp;
//     public static System.Action OnKeyUsed;

//     private Key currentKey = null;

//     void Start()
//     {
//         // If we already have a key from a previous scene, reattach it to the player
//         if (KeyManager.Instance != null && KeyManager.Instance.CarriedKey != null)
//         {
//             currentKey = KeyManager.Instance.CarriedKey;
//             currentKey.transform.SetParent(transform);
//             currentKey.transform.localPosition = Vector3.zero;
//             currentKey.transform.localRotation = Quaternion.identity;
//         }
//     }

//     void OnTriggerEnter2D(Collider2D other)
//     {
//         // Key Pickup
//         if (other.CompareTag("Key") && currentKey == null)
//         {
//             currentKey = other.GetComponent<Key>();
//             if (currentKey != null)
//             {
//                 currentKey.PickUp(transform);
//                 KeyManager.Instance.SetKey(currentKey);
//                 OnKeyPickedUp?.Invoke();
//             }
//         }

//         // Lock Interaction
//         if (other.CompareTag("Lock") && currentKey != null)
//         {
//             Lock lockObj = other.GetComponent<Lock>();
//             if (lockObj != null && lockObj.TryUnlock(currentKey.color))
//             {
//                 Destroy(currentKey.gameObject);
//                 KeyManager.Instance.DropKey();
//                 currentKey = null;
//                 OnKeyUsed?.Invoke();
//             }
//         }
//     }
// }
