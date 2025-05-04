using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

// public class FinishPointDown : MonoBehaviour
// {
//     private bool playerInTrigger = false;

//     private void Update()
//     {
//         if (playerInTrigger && Input.GetKeyDown(KeyCode.DownArrow))
//         {
//             // Load the next level or perform any other action
//             sceneController.instance.NextLevel();
//         }
//     }

//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         if (collision.CompareTag("Player"))
//         {
//             playerInTrigger = true;
//         }
//     }

//     private void OnTriggerExit2D(Collider2D collision)
//     {
//         if (collision.CompareTag("Player"))
//         {
//             playerInTrigger = false;
//         }
//     }
// }


public class FinishPointDown : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string targetSceneName;
    [SerializeField] private bool useBuildIndex = false;
    [SerializeField] private int targetBuildIndex;

    private bool playerInTrigger = false;

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.DownArrow))
        {
            LoadTargetScene();
        }
    }

    private void LoadTargetScene()
    {
        if (useBuildIndex)
        {
            SceneManager.LoadScene(targetBuildIndex); // Changed from LoadSceneAsync
        }
        else
        {
            SceneManager.LoadScene(targetSceneName); // Changed from LoadSceneAsync
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