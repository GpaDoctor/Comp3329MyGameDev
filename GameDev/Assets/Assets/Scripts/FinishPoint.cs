using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private bool playerInTrigger = false;

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Load the next level or perform any other action
            sceneController.instance.NextLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }
}
