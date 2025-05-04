using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class Lock : MonoBehaviour
// {
//     // [Header("Settings")]
//     [SerializeField] private Key.KeyColor requiredColor;
    
//     // [Header("Effects")]
//     // [SerializeField] private ParticleSystem unlockEffect;
//     // [SerializeField] private AudioClip unlockSound;
//     // [SerializeField] private SpriteRenderer lockSprite;

//     public bool TryUnlock(Key.KeyColor playerKeyColor)
//     {
//         bool success = playerKeyColor == requiredColor;
        
//         if (success)
//         {
//             // Visual feedback
//             // if (unlockEffect != null)
//             //     Instantiate(unlockEffect, transform.position, Quaternion.identity);
                
//             // if (unlockSound != null)
//             //     AudioSource.PlayClipAtPoint(unlockSound, transform.position);
            
//             // Disable the lock
//             GetComponent<SpriteRenderer>().enabled = false;
//             GetComponent<Collider2D>().enabled = false;
            
//             Destroy(gameObject, 2f); // Delay for effects to play
//         }
        
//         return success;
//     }
// }

public class Lock : MonoBehaviour
{
    [SerializeField] private Key.KeyColor requiredColor;

    [Header("Unique Lock ID (Set in Inspector)")]
    public string lockID; // Must be unique per lock in the whole game

    void Start()
    {
        // Check if this lock has already been unlocked in a previous visit
        if (GameStateManager.Instance != null && GameStateManager.Instance.IsLockUnlocked(lockID))
        {
            Destroy(gameObject);
        }
    }

    public bool TryUnlock(Key.KeyColor playerKeyColor)
    {
        bool success = playerKeyColor == requiredColor;

        if (success)
        {
            // Mark as unlocked in global game state
            GameStateManager.Instance.UnlockLock(lockID);

            // Optional visual/audio feedback
            // if (unlockEffect != null)
            //     Instantiate(unlockEffect, transform.position, Quaternion.identity);
            // if (unlockSound != null)
            //     AudioSource.PlayClipAtPoint(unlockSound, transform.position);

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            Destroy(gameObject, 2f); // Let effects play before fully destroying
        }

        return success;
    }
}
