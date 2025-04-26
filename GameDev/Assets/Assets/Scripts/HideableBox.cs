using UnityEngine;
using System.Collections;

public class HideableBox : MonoBehaviour
{
    [SerializeField] private Transform boxPosition; // Assign the box's position in the Inspector
    [SerializeField] private Collider2D boxCollider; // Assign the box's collider in the Inspector

    private GameObject currentPlayer;
    private bool isPlayerHidden = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPlayerHidden)
        {
            currentPlayer = other.gameObject;

            // Hide the player
            currentPlayer.SetActive(false);
            isPlayerHidden = true;
        }
    }

    void Update()
    {
        if (isPlayerHidden && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ReappearWithDelay());
        }
    }

    IEnumerator ReappearWithDelay()
    {
        // Temporarily disable the box collider
        boxCollider.enabled = false;

        // Reappear the player at the box's position
        currentPlayer.transform.position = boxPosition.position;
        currentPlayer.SetActive(true);
        isPlayerHidden = false;

        // Wait for 1 second before re-enabling the box collider
        yield return new WaitForSeconds(1f);

        boxCollider.enabled = true;
    }
}