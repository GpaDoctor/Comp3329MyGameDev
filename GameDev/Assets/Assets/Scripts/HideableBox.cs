using UnityEngine.SocialPlatforms.Impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class HideableBox : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject hiddenPlayerPrefab; // Assign in Inspector
    [SerializeField] private Transform hidePosition; // Assign empty child object in Inspector

    private GameObject currentPlayer;
    private GameObject hiddenPlayerInstance;
    private bool isPlayerHidden = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPlayerHidden && currentPlayer == null)
        {
            currentPlayer = other.gameObject;
            HidePlayer();
        }
    }

    void Update()
    {
        if (isPlayerHidden && Input.GetKeyDown(KeyCode.E))
        {
            RevealPlayer();
        }
    }

    void HidePlayer()
    {
        // Disable player components instead of destroying
        currentPlayer.GetComponent<SpriteRenderer>().enabled = false;
        currentPlayer.GetComponent<Collider2D>().enabled = false;
        
        // Create hidden player visual
        hiddenPlayerInstance = Instantiate(
            hiddenPlayerPrefab,
            hidePosition.position,
            hidePosition.rotation,
            transform // Parent to box
        );
        
        isPlayerHidden = true;
        Debug.Log("Player hidden!");
    }

    void RevealPlayer()
    {
        StartCoroutine(RevealPlayerWithDelay());
    }

    IEnumerator RevealPlayerWithDelay()
    {
        // Re-enable player
        currentPlayer.GetComponent<SpriteRenderer>().enabled = true;
        currentPlayer.GetComponent<Collider2D>().enabled = true;
        currentPlayer.transform.position = hidePosition.position;

        // Destroy hidden visual
        if (hiddenPlayerInstance != null)
        {
            Destroy(hiddenPlayerInstance);
        }

        isPlayerHidden = false;
        Debug.Log("Player revealed!");

        // Wait for 5 seconds
        yield return new WaitForSeconds(1f);

        // Clear reference after delay
        currentPlayer = null;
        Debug.Log("Player fully revealed after delay!");
    }
}